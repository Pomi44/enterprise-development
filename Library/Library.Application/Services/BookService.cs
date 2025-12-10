using AutoMapper;
using Library.Application.Contracts.BookLoans;
using Library.Application.Contracts.Books;
using Library.Application.Contracts.EditionTypes;
using Library.Application.Contracts.Publishers;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис книг для CRUD-операций и получения связанных данных через репозитории
/// </summary>
/// <param name="books">Репозиторий книг</param>
/// <param name="bookLoans">Репозиторий выдач книг</param>
/// <param name="editionTypes">Репозиторий видов изданий</param>
/// <param name="publishers">Репозиторий издательств</param>
/// <param name="mapper">Mapper для преобразований сущностей и DTO</param>
public class BookService(
    IRepository<Book, int> books,
    IRepository<BookLoan, int> bookLoans,
    IRepository<EditionType, int> editionTypes,
    IRepository<Publisher, int> publishers,
    IMapper mapper) : IBookService
{
    /// <summary>
    /// Создать книгу и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления книги</param>
    /// <returns>DTO для получения книги</returns>
    public async Task<BookDto> Create(BookCreateUpdateDto dto)
    {
        var entity = mapper.Map<Book>(dto);
        var created = await books.Create(entity);
        return mapper.Map<BookDto>(created);
    }

    /// <summary>
    /// Получить книгу по идентификатору или вернуть null если книга не найдена
    /// </summary>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <returns>DTO для получения книги или null</returns>
    public async Task<BookDto?> Get(int dtoId)
    {
        var entity = await books.Read(dtoId);
        return entity is null ? null : mapper.Map<BookDto>(entity);
    }

    /// <summary>
    /// Получить список всех книг
    /// </summary>
    /// <returns>Список DTO для получения книг</returns>
    public async Task<IList<BookDto>> GetAll()
    {
        var entities = await books.ReadAll();
        return [.. entities.Select(mapper.Map<BookDto>)];
    }

    /// <summary>
    /// Обновить книгу по идентификатору и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления книги</param>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <returns>DTO для получения книги</returns>
    public async Task<BookDto> Update(BookCreateUpdateDto dto, int dtoId)
    {
        var existingEntity = await books.Read(dtoId) ?? throw new KeyNotFoundException($"Книга не найдена bookId={dtoId}");

        mapper.Map(dto, existingEntity);

        var updated = await books.Update(existingEntity);
        return mapper.Map<BookDto>(updated);
    }

    /// <summary>
    /// Удалить книгу по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <returns>true если книга удалена иначе false</returns>
    public Task<bool> Delete(int dtoId)
        => books.Delete(dtoId);

    /// <summary>
    /// Получить DTO вида издания для книги по идентификатору книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>DTO для получения вида издания</returns>
    public async Task<EditionTypeDto> GetEditionType(int bookId)
    {
        var book = await books.Read(bookId) ?? throw new InvalidOperationException($"Книга не найдена bookId={bookId}");

        var editionType = book.EditionType ?? await editionTypes.Read(book.EditionTypeId);

        return editionType is null
            ? throw new InvalidOperationException($"Вид издания не найден editionTypeId={book.EditionTypeId}")
            : mapper.Map<EditionTypeDto>(editionType);
    }

    /// <summary>
    /// Получить DTO издательства для книги по идентификатору книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>DTO для получения издательства</returns>
    public async Task<PublisherDto> GetPublisher(int bookId)
    {
        var book = await books.Read(bookId) ?? throw new InvalidOperationException($"Книга не найдена bookId={bookId}");

        var publisher = book.Publisher ?? await publishers.Read(book.PublisherId);

        return publisher is null
            ? throw new InvalidOperationException($"Издательство не найдено publisherId={book.PublisherId}")
            : mapper.Map<PublisherDto>(publisher);
    }

    /// <summary>
    /// Получить список DTO выдач книги по идентификатору книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>Список DTO для получения выдач книги</returns>
    public async Task<IList<BookLoanDto>> GetLoans(int bookId)
    {
        var loans = await bookLoans.ReadAll();
        return [.. loans
            .Where(l => l.BookId == bookId)
            .OrderBy(l => l.Id)
            .Select(mapper.Map<BookLoanDto>)];
    }
}