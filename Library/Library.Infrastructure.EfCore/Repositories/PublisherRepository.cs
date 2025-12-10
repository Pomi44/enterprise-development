using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для CRUD-операций над издательствами
/// </summary>
/// <param name="db">Контекст базы данных библиотеки</param>
public class PublisherRepository(LibraryDbContext db) : IRepository<Publisher, int>
{
    /// <summary>
    /// Создать издательство и вернуть сохранённый экземпляр
    /// </summary>
    /// <param name="entity">Издательство для создания</param>
    /// <returns>Созданное издательство</returns>
    public async Task<Publisher> Create(Publisher entity)
    {
        await db.Publishers.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удалить издательство по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор издательства</param>
    /// <returns>true если издательство удалено иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.Publishers.FirstOrDefaultAsync(p => p.Id == entityId);
        if (entity is null)
            return false;

        db.Publishers.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Прочитать издательство по идентификатору или вернуть null если издательство не найдено
    /// </summary>
    /// <param name="entityId">Идентификатор издательства</param>
    /// <returns>Найденное издательство или null</returns>
    public Task<Publisher?> Read(int entityId)
    {
        return db.Publishers.FirstOrDefaultAsync(p => p.Id == entityId);
    }

    /// <summary>
    /// Прочитать все издательства
    /// </summary>
    /// <returns>Список издательств</returns>
    public async Task<IList<Publisher>> ReadAll()
    {
        return await db.Publishers
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновить издательство и вернуть обновлённый экземпляр
    /// </summary>
    /// <param name="entity">Издательство для обновления</param>
    /// <returns>Обновлённое издательство</returns>
    public async Task<Publisher> Update(Publisher entity)
    {
        db.Publishers.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}