using Library.Application.Contracts.Books;
using Library.Application.Contracts.BookLoans;
using Library.Application.Contracts.EditionTypes;
using Library.Application.Contracts.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над книгами и получения связанных данных книги
/// </summary>
/// <param name="appService">Сервис книг</param>
/// <param name="logger">Логгер контроллера</param>
public class BookController(IBookService appService, ILogger<BookController> logger)
    : CrudControllerBase<BookDto, BookCreateUpdateDto, int>(appService, logger)
{
    /// <summary>
    /// Получить вид издания книги по идентификатору книги
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>DTO для получения вида издания</returns>
    [HttpGet("{id}/EditionType")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<EditionTypeDto>> GetEditionType([FromRoute] int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetEditionType), GetType().Name, id);

        try
        {
            var res = await appService.GetEditionType(id);

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetEditionType), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "{method} method of {controller} returned not found with {id} parameter", nameof(GetEditionType), GetType().Name, id);
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "{method} method of {controller} returned not found with {id} parameter", nameof(GetEditionType), GetType().Name, id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetEditionType), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить издательство книги по идентификатору книги
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>DTO для получения издательства</returns>
    [HttpGet("{id}/Publisher")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PublisherDto>> GetPublisher([FromRoute] int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetPublisher), GetType().Name, id);

        try
        {
            var res = await appService.GetPublisher(id);

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPublisher), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "{method} method of {controller} returned not found with {id} parameter", nameof(GetPublisher), GetType().Name, id);
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "{method} method of {controller} returned not found with {id} parameter", nameof(GetPublisher), GetType().Name, id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetPublisher), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получить список выдач книги по идентификатору книги
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>Список DTO для получения выдач книги</returns>
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