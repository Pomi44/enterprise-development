using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для CRUD-операций над видами изданий
/// </summary>
/// <param name="db">Контекст базы данных библиотеки</param>
public class EditionTypeRepository(LibraryDbContext db) : IRepository<EditionType, int>
{
    /// <summary>
    /// Создать вид издания и вернуть сохранённый экземпляр
    /// </summary>
    /// <param name="entity">Вид издания для создания</param>
    /// <returns>Созданный вид издания</returns>
    public async Task<EditionType> Create(EditionType entity)
    {
        await db.EditionTypes.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удалить вид издания по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор вида издания</param>
    /// <returns>true если вид издания удалён иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.EditionTypes.FirstOrDefaultAsync(et => et.Id == entityId);
        if (entity is null)
            return false;

        db.EditionTypes.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Прочитать вид издания по идентификатору или вернуть null если вид издания не найден
    /// </summary>
    /// <param name="entityId">Идентификатор вида издания</param>
    /// <returns>Найденный вид издания или null</returns>
    public Task<EditionType?> Read(int entityId)
    {
        return db.EditionTypes.FirstOrDefaultAsync(et => et.Id == entityId);
    }

    /// <summary>
    /// Прочитать все виды изданий
    /// </summary>
    /// <returns>Список видов изданий</returns>
    public async Task<IList<EditionType>> ReadAll()
    {
        return await db.EditionTypes
            .AsNoTracking()
            .OrderBy(et => et.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновить вид издания и вернуть обновлённый экземпляр
    /// </summary>
    /// <param name="entity">Вид издания для обновления</param>
    /// <returns>Обновлённый вид издания</returns>
    public async Task<EditionType> Update(EditionType entity)
    {
        db.EditionTypes.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}