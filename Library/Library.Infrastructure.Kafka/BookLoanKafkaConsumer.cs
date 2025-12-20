using Confluent.Kafka;
using Library.Application.Contracts;
using Library.Application.Contracts.BookLoans;
using Library.Application.Contracts.Books;
using Library.Application.Contracts.Readers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Kafka;

/// <summary>
/// Kafka consumer для обработки сообщений с выдачами книг и созданием BookLoan после валидации связанных сущностей
/// </summary>
/// <param name="consumer">Экземпляр Kafka consumer</param>
/// <param name="scopeFactory">Фабрика scope для получения scoped зависимостей</param>
/// <param name="configuration">Конфигурация для чтения Kafka настроек</param>
/// <param name="logger">Логгер</param>
public class BookLoanKafkaConsumer(
    IConsumer<Guid, IList<BookLoanCreateUpdateDto>> consumer,
    ILogger<BookLoanKafkaConsumer> logger,
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly string _topic = configuration.GetSection("Kafka")["BookLoanTopicName"] ?? throw new KeyNotFoundException("BookLoanTopicName section of Kafka is missing");

    /// <summary>
    /// Запуск цикла чтения Kafka сообщений и создания выдач книг
    /// </summary>
    /// <param name="stoppingToken">Токен отмены</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        try
        {
            consumer.Subscribe(_topic);

            logger.LogInformation("Consumer {consumer} subscribed to topic {topic}", consumer.Name, _topic);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to subscribe consumer {consumer} to topic {topic}", consumer.Name, _topic);
            return;
        }

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<Guid, IList<BookLoanCreateUpdateDto>>? msg = null;

                try
                {
                    msg = consumer.Consume(stoppingToken);

                    var payload = msg?.Message?.Value;
                    if (payload is null || payload.Count == 0)
                        continue;

                    await ProcessMessage(payload, msg!.Message.Key, stoppingToken);

                    consumer.Commit(msg);

                    logger.LogInformation("Committed message {key} from topic {topic} via consumer {consumer}", msg.Message.Key, _topic, consumer.Name);
                }
                catch (ConsumeException ex) when (ex.Error.Code == ErrorCode.UnknownTopicOrPart)
                {
                    logger.LogWarning("Topic {topic} is not available yet, retrying...", _topic);
                    await Task.Delay(2000, stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to consume or process message from topic {topic}", _topic);
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
        finally
        {
            try
            {
                consumer.Close();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Error during consumer close");
            }
        }
    }

    /// <summary>
    /// Обработать одно Kafka сообщение и создать выдачи книг для валидных контрактов
    /// </summary>
    /// <param name="payload">Список DTO выдач из сообщения</param>
    /// <param name="messageKey">Ключ сообщения Kafka</param>
    /// <param name="stoppingToken">Токен отмены</param>
    private async Task ProcessMessage(IList<BookLoanCreateUpdateDto> payload, Guid messageKey, CancellationToken stoppingToken)
    {
        logger.LogInformation("Processing message {key} from topic {topic} with {count} contracts", messageKey, _topic, payload.Count);

        using var scope = scopeFactory.CreateScope();

        var bookLoans = scope.ServiceProvider.GetRequiredService<IApplicationService<BookLoanDto, BookLoanCreateUpdateDto, int>>();

        var books = scope.ServiceProvider.GetRequiredService<IApplicationService<BookDto, BookCreateUpdateDto, int>>();

        var readers = scope.ServiceProvider.GetRequiredService<IApplicationService<ReaderDto, ReaderCreateUpdateDto, int>>();

        foreach (var dto in payload)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var (isValid, error) = await Validate(dto, books, readers);
            if (!isValid)
            {
                logger.LogWarning("Skipping BookLoan contract from message {key} because {reason} BookId={bookId} ReaderId={readerId}", messageKey, error, dto.BookId, dto.ReaderId);

                continue;
            }

            try
            {
                await bookLoans.Create(dto);

                logger.LogInformation("Created book loan from message {key} BookId={bookId} ReaderId={readerId} LoanDate={loanDate}", messageKey, dto.BookId, dto.ReaderId, dto.LoanDate);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to create book loan from message {key} BookId={bookId} ReaderId={readerId}", messageKey, dto.BookId, dto.ReaderId);
            }
        }
    }

    /// <summary>
    /// Валидация DTO выдачи книги перед созданием с проверкой существования связанных сущностей
    /// </summary>
    /// <param name="dto">DTO для создания или обновления выдачи книги</param>
    /// <param name="books">Сервис книг для проверки существования книги</param>
    /// <param name="readers">Сервис читателей для проверки существования читателя</param>
    /// <returns>Признак валидности и причина ошибки для логирования</returns>
    private static async Task<(bool IsValid, string Reason)> Validate(
        BookLoanCreateUpdateDto dto,
        IApplicationService<BookDto, BookCreateUpdateDto, int> books,
        IApplicationService<ReaderDto, ReaderCreateUpdateDto, int> readers)
    {
        if (dto.BookId <= 0)
            return (false, "BookId must be more than 0");

        if (dto.ReaderId <= 0)
            return (false, "ReaderId must be more than 0");

        var book = await books.Get(dto.BookId);
        if (book is null)
            return (false, $"Book with bookId={dto.BookId} not found");

        var reader = await readers.Get(dto.ReaderId);
        if (reader is null)
            return (false, $"Reader with readerId={dto.ReaderId} not found");

        return (true, string.Empty);
    }
}