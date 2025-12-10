using Library.Application.Contracts.BookLoans;
using Library.Application.Contracts.EditionTypes;
using Library.Application.Contracts.Publishers;

namespace Library.Application.Contracts.Books;

/// <summary>
/// Контракт сервиса книг для выполнения CRUD-операций и получения связанных данных
/// </summary>
public interface IBookService : IApplicationService<BookDto, BookCreateUpdateDto, int>
{
    /// <summary>
    /// Получить DTO вида издания для книги по идентификатору книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>DTO вида издания</returns>
    public Task<EditionTypeDto> GetEditionType(int bookId);

    /// <summary>
    /// Получить DTO издательства для книги по идентификатору книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>DTO издательства</returns>
    public Task<PublisherDto> GetPublisher(int bookId);

    /// <summary>
    /// Получить список DTO выдач книги по идентификатору книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>Список DTO выдач книги</returns>
    public Task<IList<BookLoanDto>> GetLoans(int bookId);
}