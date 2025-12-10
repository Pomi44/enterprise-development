using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.EditionTypes;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис видов изданий для CRUD-операций через репозиторий
/// </summary>
/// <param name="editionTypes">Репозиторий видов изданий</param>
/// <param name="mapper">Mapper для преобразований сущностей и DTO</param>
public class EditionTypeService(
    IRepository<EditionType, int> editionTypes,
    IMapper mapper) : IApplicationService<EditionTypeDto, EditionTypeCreateUpdateDto, int>
{
    /// <summary>
    /// Создать вид издания и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления вида издания</param>
    /// <returns>DTO для получения вида издания</returns>
    public async Task<EditionTypeDto> Create(EditionTypeCreateUpdateDto dto)
    {
        var entity = mapper.Map<EditionType>(dto);
        var created = await editionTypes.Create(entity);
        return mapper.Map<EditionTypeDto>(created);
    }

    /// <summary>
    /// Получить вид издания по идентификатору или вернуть null если вид издания не найден
    /// </summary>
    /// <param name="dtoId">Идентификатор вида издания</param>
    /// <returns>DTO для получения вида издания или null</returns>
    public async Task<EditionTypeDto?> Get(int dtoId)
    {
        var entity = await editionTypes.Read(dtoId);
        return entity is null ? null : mapper.Map<EditionTypeDto>(entity);
    }

    /// <summary>
    /// Получить список всех видов изданий
    /// </summary>
    /// <returns>Список DTO для получения видов изданий</returns>
    public async Task<IList<EditionTypeDto>> GetAll()
    {
        var entities = await editionTypes.ReadAll();
        return [.. entities.Select(mapper.Map<EditionTypeDto>)];
    }

    /// <summary>
    /// Обновить вид издания по идентификатору и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления вида издания</param>
    /// <param name="dtoId">Идентификатор вида издания</param>
    /// <returns>DTO для получения вида издания</returns>
    public async Task<EditionTypeDto> Update(EditionTypeCreateUpdateDto dto, int dtoId)
    {
        var existingEntity = await editionTypes.Read(dtoId)
            ?? throw new KeyNotFoundException($"Вид издания не найден editionTypeId={dtoId}");

        mapper.Map(dto, existingEntity);

        var updated = await editionTypes.Update(existingEntity);
        return mapper.Map<EditionTypeDto>(updated);
    }

    /// <summary>
    /// Удалить вид издания по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор вида издания</param>
    /// <returns>true если вид издания удалён иначе false</returns>
    public Task<bool> Delete(int dtoId)
        => editionTypes.Delete(dtoId);
}