using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для CRUD-операций над книгами
/// </summary>
/// <param name="db">Контекст базы данных библиотеки</param>
public class BookRepository(LibraryDbContext db) : IRepository<Book, int>
{
    /// <summary>
    /// Создать книгу и вернуть сохранённый экземпляр
    /// </summary>
    /// <param name="entity">Книга для создания</param>
    /// <returns>Созданная книга</returns>
    public async Task<Book> Create(Book entity)
    {
        await db.Books.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удалить книгу по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор книги</param>
    /// <returns>true если книга удалена иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.Books.FirstOrDefaultAsync(b => b.Id == entityId);
        if (entity is null)
            return false;

        db.Books.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Прочитать книгу по идентификатору или вернуть null если книга не найдена
    /// </summary>
    /// <param name="entityId">Идентификатор книги</param>
    /// <returns>Найденная книга или null</returns>
    public Task<Book?> Read(int entityId)
    {
        return db.Books
            .Include(b => b.EditionType)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(b => b.Id == entityId);
    }

    /// <summary>
    /// Прочитать все книги
    /// </summary>
    /// <returns>Список книг</returns>
    public async Task<IList<Book>> ReadAll()
    {
        return await db.Books
            .AsNoTracking()
            .Include(b => b.EditionType)
            .Include(b => b.Publisher)
            .OrderBy(b => b.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновить книгу и вернуть обновлённый экземпляр
    /// </summary>
    /// <param name="entity">Книга для обновления</param>
    /// <returns>Обновлённая книга</returns>
    public async Task<Book> Update(Book entity)
    {
        db.Books.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}