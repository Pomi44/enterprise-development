using Library.Application.Contracts.Books;

namespace Library.Application.Contracts.Analytics;

/// <summary>
/// DTO для строки отчёта по книгам с количеством выдач за период
/// </summary>
/// <param name="Book">DTO для получения Book</param>
/// <param name="LoanCount">Количество выдач за период</param>
public record LeastPopularBookRowDto(
    BookDto Book,
    int LoanCount
);