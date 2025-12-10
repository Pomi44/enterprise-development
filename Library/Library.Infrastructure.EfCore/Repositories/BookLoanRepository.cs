using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для CRUD-операций над выдачами книг
/// </summary>
/// <param name="db">Контекст базы данных библиотеки</param>
public class BookLoanRepository(LibraryDbContext db) : IRepository<BookLoan, int>
{
    /// <summary>
    /// Создать выдачу книги и вернуть сохранённый экземпляр
    /// </summary>
    /// <param name="entity">Выдача книги для создания</param>
    /// <returns>Созданная выдача книги</returns>
    public async Task<BookLoan> Create(BookLoan entity)
    {
        await db.BookLoans.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удалить выдачу книги по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор выдачи</param>
    /// <returns>true если выдача удалена иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.BookLoans.FirstOrDefaultAsync(l => l.Id == entityId);
        if (entity is null)
            return false;

        db.BookLoans.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Прочитать выдачу книги по идентификатору или вернуть null если выдача не найдена
    /// </summary>
    /// <param name="entityId">Идентификатор выдачи</param>
    /// <returns>Найденная выдача или null</returns>
    public Task<BookLoan?> Read(int entityId)
    {
        return db.BookLoans
            .Include(l => l.Book)
            .Include(l => l.Reader)
            .FirstOrDefaultAsync(l => l.Id == entityId);
    }

    /// <summary>
    /// Прочитать все выдачи книг
    /// </summary>
    /// <returns>Список выдач книг</returns>
    public async Task<IList<BookLoan>> ReadAll()
    {
        return await db.BookLoans
            .AsNoTracking()
            .Include(l => l.Book)
            .Include(l => l.Reader)
            .OrderBy(l => l.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновить выдачу книги и вернуть обновлённый экземпляр
    /// </summary>
    /// <param name="entity">Выдача книги для обновления</param>
    /// <returns>Обновлённая выдача</returns>
    public async Task<BookLoan> Update(BookLoan entity)
    {
        db.BookLoans.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}