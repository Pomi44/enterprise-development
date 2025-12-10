using Library.Domain.DataSeeder;

namespace Library.Tests;

/// <summary>
/// Набор модульных тестов для проверки LINQ-запросов по данным библиотеки.
/// </summary>
public class LibraryQueriesTests(LibraryData data) : IClassFixture<LibraryData>
{
    /// <summary>
    /// Вывести информацию о выданных книгах, упорядоченных по названию.
    /// </summary>
    [Fact]
    public void GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder()
    {
        var expectedIds = new List<int> { 9, 7, 5, 3 };

        var resultIds = LibraryData.BookLoans
            .Where(l => l.ReturnDate is null)
            .Join(
                LibraryData.Books,
                l => l.BookId,
                b => b.Id,
                (l, b) => b)
            .OrderBy(b => b.Title)
            .Select(b => b.Id)
            .ToList();

        Assert.Equal(expectedIds.Count, resultIds.Count);
        foreach (var id in expectedIds)
            Assert.Contains(id, resultIds);
        var titles = LibraryData.BookLoans
            .Where(l => l.ReturnDate is null)
            .Join(
                LibraryData.Books,
                l => l.BookId,
                b => b.Id,
                (l, b) => b.Title)
            .OrderBy(t => t)
            .ToList();
        Assert.True(titles.SequenceEqual(titles.OrderBy(t => t)));
    }

    /// <summary>
    /// Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
    /// </summary>
    [Fact]
    public void GetTop5Readers_ByBorrowCount_ReturnsExpectedReaders()
    {
        var periodEnd = LibraryData.BookLoans.Max(l => l.LoanDate);
        var periodStart = periodEnd.AddYears(-1);

        var result = LibraryData.BookLoans
            .Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd)
            .GroupBy(l => l.ReaderId)
            .Select(g => new { ReaderId = g.Key, Count = g.Count() })
            .Join(
                LibraryData.Readers,
                x => x.ReaderId,
                r => r.Id,
                (x, r) => new { Reader = r, x.Count })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Reader.LastName)
            .Take(5)
            .ToList();

        Assert.True(result.Count <= 5);
        Assert.All(result, x => Assert.True(x.Count > 0));
        Assert.True(result.Zip(result.Skip(1), (a, b) => a.Count >= b.Count).All(x => x));

        var readersInPeriodIds = LibraryData.BookLoans
            .Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd)
            .Select(l => l.ReaderId)
            .Distinct()
            .ToHashSet();

        Assert.All(result, x => Assert.Contains(x.Reader.Id, readersInPeriodIds));
    }

    /// <summary>
    /// Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО.
    /// </summary>
    [Fact]
    public void GetReaders_WithMaxLoanPeriod_ReturnsExpectedReader()
    {
        var maxDays = LibraryData.BookLoans.Max(l => l.Days);

        var readers = LibraryData.BookLoans
            .Where(l => l.Days == maxDays)
            .Select(l => l.ReaderId)
            .Distinct()
            .Join(
                LibraryData.Readers,
                id => id,
                r => r.Id,
                (id, r) => r)
            .OrderBy(r => r.LastName)
            .ToList();

        Assert.NotEmpty(readers);

        Assert.All(readers, r =>
            Assert.Contains(LibraryData.BookLoans, l => l.ReaderId == r.Id && l.Days == maxDays));

        Assert.True(
            readers.Zip(
                    readers.Skip(1),
                    (a, b) => string.Compare(a.LastName, b.LastName, StringComparison.Ordinal) <= 0)
                .All(x => x));

        Assert.Contains(readers, r => r.LastName == "Васильев");
    }

    /// <summary>
    /// Вывести топ 5 наиболее популярных издательств за последний год.
    /// </summary>
    [Fact]
    public void GetTop5PopularPublishers_LastYear_ReturnsExpectedCount()
    {
        var periodEnd = LibraryData.BookLoans.Max(l => l.LoanDate);
        var periodStart = periodEnd.AddYears(-1);

        var result = LibraryData.BookLoans
            .Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd)
            .Join(
                LibraryData.Books,
                l => l.BookId,
                b => b.Id,
                (l, b) => b.PublisherId)
            .GroupBy(publisherId => publisherId)
            .Select(g => new { PublisherId = g.Key, Count = g.Count() })
            .Join(
                LibraryData.Publishers,
                x => x.PublisherId,
                p => p.Id,
                (x, p) => new { Publisher = p.Name, x.Count })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        Assert.True(result.Count <= 5);
        Assert.All(result, x => Assert.True(x.Count > 0));
        Assert.True(result.Zip(result.Skip(1), (a, b) => a.Count >= b.Count).All(x => x));
    }

    /// <summary>
    /// Вывести топ 5 наименее популярных книг за последний год.
    /// </summary>
    [Fact]
    public void GetTop5LeastPopularBooks_LastYear_ReturnsExpectedBooks()
    {
        var periodEnd = LibraryData.BookLoans.Max(l => l.LoanDate);
        var periodStart = periodEnd.AddYears(-1);

        var loansInPeriod = LibraryData.BookLoans
            .Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd);

        var result = LibraryData.Books
            .GroupJoin(
                loansInPeriod,
                b => b.Id,
                l => l.BookId,
                (b, loans) => new { Book = b, Count = loans.Count() })
            .OrderBy(x => x.Count)
            .ThenBy(x => x.Book.Title)
            .Take(5)
            .ToList();

        Assert.Equal(5, result.Count);
        Assert.True(result.Zip(result.Skip(1), (a, b) => a.Count <= b.Count).All(x => x));
        Assert.True(result.Any(x => x.Count == 0) || result.First().Count <= result.Last().Count);
    }
}
