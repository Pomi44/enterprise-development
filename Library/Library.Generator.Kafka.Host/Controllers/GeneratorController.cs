using Library.Application.Contracts.BookLoans;
using Library.Generator.Kafka.Host.Generator;
using Microsoft.AspNetCore.Mvc;

namespace Library.Generator.Kafka.Host.Controllers;

/// <summary>
/// Контроллер генерации тестовых данных и отправки их в Kafka
/// </summary>
/// <param name="producer">Kafka producer выдач книг</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
public class GeneratorController(
    BookLoanKafkaProducer producer,
    ILogger<GeneratorController> logger) : ControllerBase
{
    /// <summary>
    /// Генерация выдачи книг и отправка в Kafka батчами с задержкой
    /// </summary>
    /// <param name="listSize">Общий размер списка генерируемых DTO</param>
    /// <param name="batchSize">Размер каждого батча</param>
    /// <param name="delayMs">Задержка между отправками батчей в миллисекундах</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список DTO для создания или обновления выдач книг</returns>
    [HttpPost("bookloans")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookLoanCreateUpdateDto>>> GenerateBookLoans(
        [FromQuery] int listSize,
        [FromQuery] int batchSize,
        [FromQuery] int delayMs,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("{method} called with listSize={listSize} batchSize={batchSize} delayMs={delayMs}", nameof(GenerateBookLoans), listSize, batchSize, delayMs);

        if (listSize <= 0)
            return BadRequest("listSize must be more than 0");

        if (batchSize <= 0)
            return BadRequest("batchSize must be more than 0");

        if (delayMs < 0)
            return BadRequest("delayMs must be 0 or more");

        try
        {
            var items = BookLoanGenerator.Generate(listSize);

            foreach (var batch in items.Chunk(batchSize))
            {
                cancellationToken.ThrowIfCancellationRequested();

                await producer.SendAsync([.. batch], cancellationToken);

                await Task.Delay(delayMs, cancellationToken);
            }

            logger.LogInformation(
                "{method} executed successfully listSize={listSize} batchSize={batchSize} delayMs={delayMs}",
                nameof(GenerateBookLoans),
                listSize,
                batchSize,
                delayMs);

            return Ok(items);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("{method} cancelled by request", nameof(GenerateBookLoans));
            return BadRequest("Request cancelled");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method}", nameof(GenerateBookLoans));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}