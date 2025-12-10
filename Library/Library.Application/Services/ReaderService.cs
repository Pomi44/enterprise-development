using AutoMapper;
using Library.Application.Contracts.BookLoans;
using Library.Application.Contracts.Readers;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис читателей для CRUD-операций и получения связанных данных через репозитории
/// </summary>
/// <param name="readers">Репозиторий читателей</param>
/// <param name="bookLoans">Репозиторий выдач книг</param>
/// <param name="mapper">Mapper для преобразований сущностей и DTO</param>
public class ReaderService(
    IRepository<Reader, int> readers,
    IRepository<BookLoan, int> bookLoans,
    IMapper mapper) : IReaderService
{
    /// <summary>
    /// Создать читателя и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления читателя</param>
    /// <returns>DTO для получения читателя</returns>
    public async Task<ReaderDto> Create(ReaderCreateUpdateDto dto)
    {
        var entity = mapper.Map<Reader>(dto);
        var created = await readers.Create(entity);
        return mapper.Map<ReaderDto>(created);
    }

    /// <summary>
    /// Получить читателя по идентификатору или вернуть null если читатель не найден
    /// </summary>
    /// <param name="dtoId">Идентификатор читателя</param>
    /// <returns>DTO для получения читателя или null</returns>
    public async Task<ReaderDto?> Get(int dtoId)
    {
        var entity = await readers.Read(dtoId);
        return entity is null ? null : mapper.Map<ReaderDto>(entity);
    }

    /// <summary>
    /// Получить список всех читателей
    /// </summary>
    /// <returns>Список DTO для получения читателей</returns>
    public async Task<IList<ReaderDto>> GetAll()
    {
        var entities = await readers.ReadAll();
        return [.. entities.Select(mapper.Map<ReaderDto>)];
    }

    /// <summary>
    /// Обновить читателя по идентификатору и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания или обновления читателя</param>
    /// <param name="dtoId">Идентификатор читателя</param>
    /// <returns>DTO для получения читателя</returns>
    public async Task<ReaderDto> Update(ReaderCreateUpdateDto dto, int dtoId)
    {
        var existingEntity = await readers.Read(dtoId)
            ?? throw new KeyNotFoundException($"Читатель не найден readerId={dtoId}");

        mapper.Map(dto, existingEntity);

        var updated = await readers.Update(existingEntity);
        return mapper.Map<ReaderDto>(updated);
    }

    /// <summary>
    /// Удалить читателя по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор читателя</param>
    /// <returns>true если читатель удалён иначе false</returns>
    public Task<bool> Delete(int dtoId)
        => readers.Delete(dtoId);

    /// <summary>
    /// Получить список DTO выдач читателя по идентификатору читателя
    /// </summary>
    /// <param name="readerId">Идентификатор читателя</param>
    /// <returns>Список DTO для получения выдач читателя</returns>
    public async Task<IList<BookLoanDto>> GetLoans(int readerId)
    {
        var loans = await bookLoans.ReadAll();
        return [.. loans
            .Where(l => l.ReaderId == readerId)
            .OrderBy(l => l.Id)
            .Select(mapper.Map<BookLoanDto>)];
    }
}