using Library.Application.Contracts;
using Library.Application.Contracts.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над издательствами через базовый CRUD-контроллер
/// </summary>
/// <param name="service">Прикладной сервис для CRUD-операций над издательствами</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
public class PublisherController(IApplicationService<PublisherDto, PublisherCreateUpdateDto, int> service, ILogger<PublisherController> logger)
    : CrudControllerBase<PublisherDto, PublisherCreateUpdateDto, int>(service, logger);