using Library.Application.Contracts;
using Library.Application.Contracts.BookLoans;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над выдачами книг через базовый CRUD-контроллер
/// </summary>
/// <param name="service">Прикладной сервис для CRUD-операций над выдачами книг</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
public class BookLoanController(IApplicationService<BookLoanDto, BookLoanCreateUpdateDto, int> service, ILogger<BookLoanController> logger)
    : CrudControllerBase<BookLoanDto, BookLoanCreateUpdateDto, int>(service, logger);