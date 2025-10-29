using Library.Domain.DataSeeder;

namespace Library.Tests;

/// <summary>
/// Набор модульных тестов для проверки LINQ-запросов по данным библиотеки.
/// </summary>
public class LibraryQueriesTests(LibraryData data) : IClassFixture<LibraryData>
{
    private readonly LibraryData _data = data;

    /// <summary>
    /// Вывести информацию о выданных книгах, упорядоченных по названию.
    /// </summary>
    [Fact]
    public void GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder()
    {
        var expectedIds = new List<int> { 9, 7, 5, 3 };

        var resultIds = LibraryData.BookLoans
            .Where(b => b.ReturnDate is null)
            .OrderBy(b => b.Book.Title)
            .Select(b => b.Book.Id)
            .ToList();

        Assert.Equal(expectedIds.Count, resultIds.Count);
        foreach (var id in expectedIds)
            Assert.Contains(id, resultIds);
        var titles = LibraryData.BookLoans
            .Where(b => b.ReturnDate is null)
            .Select(b => b.Book.Title)
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
            .GroupBy(l => l.Reader)
            .Select(g => new { Reader = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Reader.LastName)
            .Take(5)
            .ToList();

        Assert.True(result.Count <= 5);
        Assert.All(result, x => Assert.True(x.Count > 0));
        Assert.True(result.Zip(result.Skip(1), (a, b) => a.Count >= b.Count).All(x => x));
        var readersInPeriod = LibraryData.BookLoans
            .Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd)
            .Select(l => l.Reader)
            .Distinct()
            .ToHashSet();
        Assert.All(result, x => Assert.Contains(x.Reader, readersInPeriod));
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
            .Select(l => l.Reader)
            .Distinct()
            .OrderBy(r => r.LastName)
            .ToList();

        Assert.NotEmpty(readers);
        Assert.All(readers, r =>
            Assert.Contains(LibraryData.BookLoans, l => l.Reader == r && l.Days == maxDays));
        Assert.True(readers.Zip(readers.Skip(1), (a, b) => string.Compare(a.LastName, b.LastName, StringComparison.Ordinal) <= 0).All(x => x));
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
            .GroupBy(l => l.Book.Publisher)
            .Select(g => new { Publisher = g.Key!.Name, Count = g.Count() })
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

        var result = LibraryData.Books
            .GroupJoin(
                LibraryData.BookLoans.Where(l => l.LoanDate >= periodStart && l.LoanDate <= periodEnd),
                b => b.Id,
                l => l.Book.Id,
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
