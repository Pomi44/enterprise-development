using Library.Application.Contracts.Analytics;
using Library.Application.Contracts.Books;

namespace Library.Application.Contracts;

/// <summary>
/// Контракт аналитического сервиса для запросов по данным библиотеки
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Вывести информацию о выданных книгах, упорядоченных по названию
    /// </summary>
    /// <returns>Список DTO для получения книг</returns>
    public Task<IList<BookDto>> GetBorrowedBooksOrderedByTitle();

    /// <summary>
    /// Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    public Task<IList<TopReaderByBorrowCountRowDto>> GetTop5ReadersByBorrowCount(DateTime periodStart, DateTime periodEnd);

    /// <summary>
    /// Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО
    /// </summary>
    /// <returns>Список строк отчёта</returns>
    public Task<IList<ReaderWithMaxLoanPeriodRowDto>> GetReadersWithMaxLoanPeriodOrderedByName();

    /// <summary>
    /// Вывести топ 5 наиболее популярных издательств за последний год
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    public Task<IList<TopPublisherRowDto>> GetTop5PopularPublishers(DateTime periodStart, DateTime periodEnd);

    /// <summary>
    /// Вывести топ 5 наименее популярных книг за последний год
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    public Task<IList<LeastPopularBookRowDto>> GetTop5LeastPopularBooks(DateTime periodStart, DateTime periodEnd);
}