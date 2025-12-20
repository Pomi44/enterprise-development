using Confluent.Kafka;
using Library.Application.Contracts.BookLoans;

namespace Library.Generator.Kafka.Host;

/// <summary>
/// Kafka producer для отправки пачек DTO выдач книг в указанный топик
/// </summary>
/// <param name="configuration">Конфигурация для чтения Kafka настроек</param>
/// <param name="producer">Kafka producer</param>
/// <param name="logger">Логгер</param>
public class BookLoanKafkaProducer(
    IConfiguration configuration,
    IProducer<Guid, IList<BookLoanCreateUpdateDto>> producer,
    ILogger<BookLoanKafkaProducer> logger)
{
    private readonly string _topic = configuration.GetSection("Kafka")["BookLoanTopicName"] ?? throw new KeyNotFoundException("BookLoanTopicName section of Kafka is missing");

    /// <summary>
    /// Отправить пачку DTO выдач книг в Kafka
    /// </summary>
    /// <param name="batch">Пачка DTO для отправки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task SendAsync(IList<BookLoanCreateUpdateDto> batch, CancellationToken cancellationToken = default)
    {
        if (batch is null || batch.Count == 0)
        {
            logger.LogWarning("Skipping send because batch is empty");
            return;
        }

        var key = Guid.NewGuid();

        try
        {
            logger.LogInformation("Sending a batch of {count} contracts to {topic} key={key}", batch.Count, _topic, key);

            var message = new Message<Guid, IList<BookLoanCreateUpdateDto>>
            {
                Key = key,
                Value = batch
            };

            var delivery = await producer.ProduceAsync(_topic, message, cancellationToken);

            logger.LogInformation(
                "Batch sent to {topic} partition={partition} offset={offset} key={key} count={count}",
                delivery.Topic,
                delivery.Partition.Value,
                delivery.Offset.Value,
                key,
                batch.Count);
        }
        catch (ProduceException<Guid, IList<BookLoanCreateUpdateDto>> ex)
        {
            logger.LogError(ex, "Kafka produce failed topic={topic} reason={reason} key={key} count={count}", _topic, ex.Error.Reason, key, batch.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occurred during sending a batch of {count} contracts to {topic} key={key}", batch.Count, _topic, key);
        }
    }
}