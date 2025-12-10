using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.BookLoans;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис выдач книг для CRUD-операций через репозиторий
/// </summary>
/// <param name="bookLoans">Репозиторий выдач книг</param>
/// <param name="mapper">Mapper для преобразований сущностей и DTO</param>
public class BookLoanService(
    IRepository<BookLoan, int> bookLoans,
    IMapper mapper) : IApplicationService<BookLoanDto, BookLoanCreateUpdateDto, int>
{
    /// <summary>
    /// Создать выдачу книги и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления выдачи книги</param>
    /// <returns>DTO для получения выдачи книги</returns>
    public async Task<BookLoanDto> Create(BookLoanCreateUpdateDto dto)
    {
        var entity = mapper.Map<BookLoan>(dto);
        var created = await bookLoans.Create(entity);
        return mapper.Map<BookLoanDto>(created);
    }

    /// <summary>
    /// Получить выдачу книги по идентификатору или вернуть null если выдача не найдена
    /// </summary>
    /// <param name="dtoId">Идентификатор выдачи</param>
    /// <returns>DTO для получения выдачи книги или null</returns>
    public async Task<BookLoanDto?> Get(int dtoId)
    {
        var entity = await bookLoans.Read(dtoId);
        return entity is null ? null : mapper.Map<BookLoanDto>(entity);
    }

    /// <summary>
    /// Получить список всех выдач книг
    /// </summary>
    /// <returns>Список DTO для получения выдач книг</returns>
    public async Task<IList<BookLoanDto>> GetAll()
    {
        var entities = await bookLoans.ReadAll();
        return [.. entities.Select(mapper.Map<BookLoanDto>)];
    }

    /// <summary>
    /// Обновить выдачу книги по идентификатору и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления выдачи книги</param>
    /// <param name="dtoId">Идентификатор выдачи</param>
    /// <returns>DTO для получения выдачи книги</returns>
    public async Task<BookLoanDto> Update(BookLoanCreateUpdateDto dto, int dtoId)
    {
        var existingEntity = await bookLoans.Read(dtoId)
            ?? throw new KeyNotFoundException($"Выдача не найдена loanId={dtoId}");

        mapper.Map(dto, existingEntity);

        var updated = await bookLoans.Update(existingEntity);
        return mapper.Map<BookLoanDto>(updated);
    }

    /// <summary>
    /// Удалить выдачу книги по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор выдачи</param>
    /// <returns>true если выдача удалена иначе false</returns>
    public Task<bool> Delete(int dtoId)
        => bookLoans.Delete(dtoId);
}