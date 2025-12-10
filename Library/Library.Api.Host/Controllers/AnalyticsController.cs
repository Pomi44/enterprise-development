using Library.Application.Contracts;
using Library.Application.Contracts.Analytics;
using Library.Application.Contracts.Books;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для аналитических запросов по данным библиотеки
/// </summary>
/// <param name="analytics">Аналитический сервис</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
public sealed class AnalyticsController(IAnalyticsService analytics, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Вывести информацию о выданных книгах, упорядоченных по названию
    /// </summary>
    /// <returns>Список DTO для получения книг</returns>
    [HttpGet("BorrowedBooks")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookDto>>> GetBorrowedBooksOrderedByTitle()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetBorrowedBooksOrderedByTitle), GetType().Name);

        try
        {
            var res = await analytics.GetBorrowedBooksOrderedByTitle();

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetBorrowedBooksOrderedByTitle), GetType().Name);

            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetBorrowedBooksOrderedByTitle), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    [HttpGet("TopReaders")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TopReaderByBorrowCountRowDto>>> GetTop5ReadersByBorrowCount([FromQuery] DateTime periodStart, [FromQuery] DateTime periodEnd)
    {
        logger.LogInformation(
            "{method} method of {controller} is called with {start},{end} parameters",
            nameof(GetTop5ReadersByBorrowCount),
            GetType().Name,
            periodStart,
            periodEnd);

        if (periodStart > periodEnd)
            return BadRequest("periodStart must be less or equal to periodEnd");

        try
        {
            var res = await analytics.GetTop5ReadersByBorrowCount(periodStart, periodEnd);

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5ReadersByBorrowCount), GetType().Name);

            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetTop5ReadersByBorrowCount), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО
    /// </summary>
    /// <returns>Список строк отчёта</returns>
    [HttpGet("ReadersWithMaxLoanPeriod")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ReaderWithMaxLoanPeriodRowDto>>> GetReadersWithMaxLoanPeriodOrderedByName()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetReadersWithMaxLoanPeriodOrderedByName), GetType().Name);

        try
        {
            var res = await analytics.GetReadersWithMaxLoanPeriodOrderedByName();

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetReadersWithMaxLoanPeriodOrderedByName), GetType().Name);

            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetReadersWithMaxLoanPeriodOrderedByName), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Вывести топ 5 наиболее популярных издательств за последний год
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    [HttpGet("TopPublishers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TopPublisherRowDto>>> GetTop5PopularPublishers(
        [FromQuery] DateTime periodStart,
        [FromQuery] DateTime periodEnd)
    {
        logger.LogInformation(
            "{method} method of {controller} is called with {start},{end} parameters",
            nameof(GetTop5PopularPublishers),
            GetType().Name,
            periodStart,
            periodEnd);

        if (periodStart > periodEnd)
            return BadRequest("periodStart must be less or equal to periodEnd");

        try
        {
            var res = await analytics.GetTop5PopularPublishers(periodStart, periodEnd);

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5PopularPublishers), GetType().Name);

            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetTop5PopularPublishers), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Вывести топ 5 наименее популярных книг за последний год
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    [HttpGet("LeastPopularBooks")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<LeastPopularBookRowDto>>> GetTop5LeastPopularBooks(
        [FromQuery] DateTime periodStart,
        [FromQuery] DateTime periodEnd)
    {
        logger.LogInformation(
            "{method} method of {controller} is called with {start},{end} parameters",
            nameof(GetTop5LeastPopularBooks),
            GetType().Name,
            periodStart,
            periodEnd);

        if (periodStart > periodEnd)
            return BadRequest("periodStart must be less or equal to periodEnd");

        try
        {
            var res = await analytics.GetTop5LeastPopularBooks(periodStart, periodEnd);

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5LeastPopularBooks), GetType().Name);

            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetTop5LeastPopularBooks), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}