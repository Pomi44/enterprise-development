using Library.Domain.DataSeeder;

namespace Library.Tests;

/// <summary>
/// Набор модульных тестов для проверки LINQ-запросов по данным библиотеки.
/// Использует IClassFixture для доступа к данным из Data.cs.
/// </summary>
public class LibraryQueriesTests : IClassFixture<LibraryData>
{
    private readonly LibraryData _data;

    public LibraryQueriesTests(LibraryData data)
    {
        _data = data;
    }

    /// <summary>
    ///Вывести информацию о выданных книгах, упорядоченных по названию.
    /// </summary>
    [Fact]
    public void GetBorrowedBooks_OrderedByBookTitle_ReturnsExpectedOrder()
    {
        var expectedIds = new List<string> { "b9", "b7", "b5", "b3" };

        var resultIds = LibraryData.BookLoans
            .Where(b => b.ReturnDate is null)
            .OrderBy(b => b.Book.Title)
            .Select(b => b.Book.Id)
            .ToList();

        Assert.Equal(expectedIds, resultIds);
    }

    /// <summary>
    /// Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
    /// </summary>
    [Fact]
    public void GetTop5Readers_ByBorrowCount_ReturnsExpectedReaders()
    {
        var expectedCount = 5;

        var result = LibraryData.BookLoans
            .GroupBy(l => l.Reader)
            .Select(g => new { Reader = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Reader.LastName)
            .Take(expectedCount)
            .ToList();

        Assert.Equal(expectedCount, result.Count);
        Assert.All(result, x => Assert.True(x.Count >= 1));
    }

    /// <summary>
    /// Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО.
    /// </summary>
    [Fact]
    public void GetReaders_WithMaxLoanPeriod_ReturnsExpectedReader()
    {
        var maxDays = LibraryData.BookLoans.Max(l => l.Days);

        var result = LibraryData.BookLoans
            .Where(l => l.Days == maxDays)
            .Select(l => l.Reader)
            .Distinct()
            .OrderBy(r => r.LastName)
            .Select(r => r.LastName)
            .ToList();

        Assert.Single(result);
        Assert.Equal("Васильев", result.First());
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

        Assert.Equal(5, result.Count);
        Assert.True(result.First().Count >= result.Last().Count);
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
        Assert.True(result.Take(3).All(x => x.Count == 0));
    }
}
