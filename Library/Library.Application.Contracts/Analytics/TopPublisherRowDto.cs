using Library.Application.Contracts.Publishers;

namespace Library.Application.Contracts.Analytics;

/// <summary>
/// DTO для строки отчёта по издательствам с количеством выдач за период
/// </summary>
/// <param name="Publisher">DTO для получения Publisher</param>
/// <param name="LoanCount">Количество выдач за период</param>
public record TopPublisherRowDto(
    PublisherDto Publisher,
    int LoanCount
);