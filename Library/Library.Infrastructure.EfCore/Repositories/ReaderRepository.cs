using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для CRUD-операций над читателями
/// </summary>
/// <param name="db">Контекст базы данных библиотеки</param>
public class ReaderRepository(LibraryDbContext db) : IRepository<Reader, int>
{
    /// <summary>
    /// Создать читателя и вернуть сохранённый экземпляр
    /// </summary>
    /// <param name="entity">Читатель для создания</param>
    /// <returns>Созданный читатель</returns>
    public async Task<Reader> Create(Reader entity)
    {
        await db.Readers.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удалить читателя по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор читателя</param>
    /// <returns>true если читатель удалён иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.Readers.FirstOrDefaultAsync(r => r.Id == entityId);
        if (entity is null)
            return false;

        db.Readers.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Прочитать читателя по идентификатору или вернуть null если читатель не найден
    /// </summary>
    /// <param name="entityId">Идентификатор читателя</param>
    /// <returns>Найденный читатель или null</returns>
    public Task<Reader?> Read(int entityId)
    {
        return db.Readers.FirstOrDefaultAsync(r => r.Id == entityId);
    }

    /// <summary>
    /// Прочитать всех читателей
    /// </summary>
    /// <returns>Список читателей</returns>
    public async Task<IList<Reader>> ReadAll()
    {
        return await db.Readers
            .AsNoTracking()
            .OrderBy(r => r.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновить читателя и вернуть обновлённый экземпляр
    /// </summary>
    /// <param name="entity">Читатель для обновления</param>
    /// <returns>Обновлённый читатель</returns>
    public async Task<Reader> Update(Reader entity)
    {
        db.Readers.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}