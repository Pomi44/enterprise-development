using Library.Application.Contracts.BookLoans;

namespace Library.Application.Contracts.Readers;

/// <summary>
/// Контракт сервиса читателей для выполнения CRUD-операций и получения связанных данных
/// </summary>
public interface IReaderService : IApplicationService<ReaderDto, ReaderCreateUpdateDto, int>
{
    /// <summary>
    /// Получить список DTO выдач читателя по идентификатору читателя
    /// </summary>
    /// <param name="readerId">Идентификатор читателя</param>
    /// <returns>Список DTO выдач читателя</returns>
    public Task<IList<BookLoanDto>> GetLoans(int readerId);
}