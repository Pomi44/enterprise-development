using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.Analytics;
using Library.Application.Contracts.Books;
using Library.Application.Contracts.Publishers;
using Library.Application.Contracts.Readers;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Реализация аналитического сервиса для запросов по данным библиотеки через репозитории
/// </summary>
/// <param name="books">Репозиторий книг</param>
/// <param name="bookLoans">Репозиторий выдач</param>
/// <param name="readers">Репозиторий читателей</param>
/// <param name="publishers">Репозиторий издательств</param>
/// <param name="mapper">Mapper для преобразований сущностей и DTO</param>
public sealed class AnalyticsService(
    IRepository<Book, int> books,
    IRepository<BookLoan, int> bookLoans,
    IRepository<Reader, int> readers,
    IRepository<Publisher, int> publishers,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Вывести информацию о выданных книгах, упорядоченных по названию
    /// </summary>
    /// <returns>Список DTO для получения книг</returns>
    public async Task<IList<BookDto>> GetBorrowedBooksOrderedByTitle()
    {
        var loans = await bookLoans.ReadAll();
        var allBooks = await books.ReadAll();

        var borrowedBooks = loans
            .Where(l => l.ReturnDate is null)
            .Join(
                allBooks,
                l => l.BookId,
                b => b.Id,
                (l, b) => b)
            .OrderBy(b => b.Title)
            .Select(mapper.Map<BookDto>)
            .ToList();

        return borrowedBooks;
    }

    /// <summary>
    /// Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    public async Task<IList<TopReaderByBorrowCountRowDto>> GetTop5ReadersByBorrowCount(DateTime periodStart, DateTime periodEnd)
    {
        var loans = await bookLoans.ReadAll();
        var allReaders = await readers.ReadAll();

        var result = loans
            .Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd)
            .GroupBy(l => l.ReaderId)
            .Select(g => new { ReaderId = g.Key, Count = g.Count() })
            .Join(
                allReaders,
                x => x.ReaderId,
                r => r.Id,
                (x, r) => new { Reader = r, x.Count })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Reader.LastName)
            .Take(5)
            .Select(x => new TopReaderByBorrowCountRowDto(
                mapper.Map<ReaderDto>(x.Reader),
                x.Count))
            .ToList();

        return result;
    }

    /// <summary>
    /// Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО
    /// </summary>
    /// <returns>Список строк отчёта</returns>
    public async Task<IList<ReaderWithMaxLoanPeriodRowDto>> GetReadersWithMaxLoanPeriodOrderedByName()
    {
        var loans = await bookLoans.ReadAll();
        var allReaders = await readers.ReadAll();

        var maxDays = loans.Max(l => l.Days);

        var result = loans
            .Where(l => l.Days == maxDays)
            .Select(l => l.ReaderId)
            .Distinct()
            .Join(
                allReaders,
                id => id,
                r => r.Id,
                (id, r) => r)
            .OrderBy(r => r.LastName)
            .ThenBy(r => r.Name)
            .ThenBy(r => r.Patronymic)
            .Select(r => new ReaderWithMaxLoanPeriodRowDto(
                mapper.Map<ReaderDto>(r),
                maxDays))
            .ToList();

        return result;
    }

    /// <summary>
    /// Вывести топ 5 наиболее популярных издательств за последний год
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    public async Task<IList<TopPublisherRowDto>> GetTop5PopularPublishers(DateTime periodStart, DateTime periodEnd)
    {
        var loans = await bookLoans.ReadAll();
        var allBooks = await books.ReadAll();
        var allPublishers = await publishers.ReadAll();

        var result = loans
            .Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd)
            .Join(
                allBooks,
                l => l.BookId,
                b => b.Id,
                (l, b) => b.PublisherId)
            .GroupBy(publisherId => publisherId)
            .Select(g => new { PublisherId = g.Key, Count = g.Count() })
            .Join(
                allPublishers,
                x => x.PublisherId,
                p => p.Id,
                (x, p) => new { Publisher = p, x.Count })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .Select(x => new TopPublisherRowDto(
                mapper.Map<PublisherDto>(x.Publisher),
                x.Count))
            .ToList();

        return result;
    }

    /// <summary>
    /// Вывести топ 5 наименее популярных книг за последний год
    /// </summary>
    /// <param name="periodStart">Начало периода</param>
    /// <param name="periodEnd">Конец периода</param>
    /// <returns>Список строк отчёта</returns>
    public async Task<IList<LeastPopularBookRowDto>> GetTop5LeastPopularBooks(DateTime periodStart, DateTime periodEnd)
    {
        var allBooks = await books.ReadAll();
        var loans = await bookLoans.ReadAll();

        var loansInPeriod = loans
            .Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd);

        var result = allBooks
            .GroupJoin(
                loansInPeriod,
                b => b.Id,
                l => l.BookId,
                (b, ls) => new { Book = b, Count = ls.Count() })
            .OrderBy(x => x.Count)
            .ThenBy(x => x.Book.Title)
            .Take(5)
            .Select(x => new LeastPopularBookRowDto(
                mapper.Map<BookDto>(x.Book),
                x.Count))
            .ToList();

        return result;
    }
}