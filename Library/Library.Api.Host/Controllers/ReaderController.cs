using Library.Application.Contracts.BookLoans;
using Library.Application.Contracts.Readers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над читателями и получения связанных данных читателя
/// </summary>
/// <param name="appService">Сервис читателей</param>
/// <param name="logger">Логгер контроллера</param>
public class ReaderController(IReaderService appService, ILogger<ReaderController> logger)
    : CrudControllerBase<ReaderDto, ReaderCreateUpdateDto, int>(appService, logger)
{
    /// <summary>
    /// Получить список выдач читателя по идентификатору читателя
    /// </summary>
    /// <param name="id">Идентификатор читателя</param>
    /// <returns>Список DTO для получения выдач читателя</returns>
    [HttpGet("{id}/Loans")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookLoanDto>>> GetLoans([FromRoute] int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetLoans), GetType().Name, id);

        try
        {
            var res = await appService.GetLoans(id);

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetLoans), GetType().Name);

            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetLoans), GetType().Name);

            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}