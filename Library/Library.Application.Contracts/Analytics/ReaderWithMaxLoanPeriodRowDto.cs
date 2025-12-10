using Library.Application.Contracts.Readers;

namespace Library.Application.Contracts.Analytics;

/// <summary>
/// DTO для строки отчёта по читателям с максимальным сроком выдачи
/// </summary>
/// <param name="Reader">DTO для получения Reader</param>
/// <param name="MaxDays">Максимальный срок выдачи в днях</param>
public record ReaderWithMaxLoanPeriodRowDto(
    ReaderDto Reader,
    int MaxDays
);