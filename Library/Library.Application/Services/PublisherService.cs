using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.Publishers;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис издательств для CRUD-операций через репозиторий
/// </summary>
/// <param name="publishers">Репозиторий издательств</param>
/// <param name="mapper">Mapper для преобразований сущностей и DTO</param>
public class PublisherService(
    IRepository<Publisher, int> publishers,
    IMapper mapper) : IApplicationService<PublisherDto, PublisherCreateUpdateDto, int>
{
    /// <summary>
    /// Создать издательство и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления издательства</param>
    /// <returns>DTO для получения издательства</returns>
    public async Task<PublisherDto> Create(PublisherCreateUpdateDto dto)
    {
        var entity = mapper.Map<Publisher>(dto);
        var created = await publishers.Create(entity);
        return mapper.Map<PublisherDto>(created);
    }

    /// <summary>
    /// Получить издательство по идентификатору или вернуть null если издательство не найдено
    /// </summary>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <returns>DTO для получения издательства или null</returns>
    public async Task<PublisherDto?> Get(int dtoId)
    {
        var entity = await publishers.Read(dtoId);
        return entity is null ? null : mapper.Map<PublisherDto>(entity);
    }

    /// <summary>
    /// Получить список всех издательств
    /// </summary>
    /// <returns>Список DTO для получения издательств</returns>
    public async Task<IList<PublisherDto>> GetAll()
    {
        var entities = await publishers.ReadAll();
        return [.. entities.Select(mapper.Map<PublisherDto>)];
    }

    /// <summary>
    /// Обновить издательство по идентификатору и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления издательства</param>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <returns>DTO для получения издательства</returns>
    public async Task<PublisherDto> Update(PublisherCreateUpdateDto dto, int dtoId)
    {
        var existingEntity = await publishers.Read(dtoId)
            ?? throw new KeyNotFoundException($"Издательство не найдено publisherId={dtoId}");

        mapper.Map(dto, existingEntity);

        var updated = await publishers.Update(existingEntity);
        return mapper.Map<PublisherDto>(updated);
    }

    /// <summary>
    /// Удалить издательство по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <returns>true если издательство удалено иначе false</returns>
    public Task<bool> Delete(int dtoId)
        => publishers.Delete(dtoId);
}