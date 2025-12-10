using Library.Application.Contracts;
using Library.Application.Contracts.EditionTypes;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над видами изданий через базовый CRUD-контроллер
/// </summary>
/// <param name="service">Прикладной сервис для CRUD-операций над видами изданий</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
public class EditionTypeController(IApplicationService<EditionTypeDto, EditionTypeCreateUpdateDto, int> service, ILogger<EditionTypeController> logger)
    : CrudControllerBase<EditionTypeDto, EditionTypeCreateUpdateDto, int>(service, logger);