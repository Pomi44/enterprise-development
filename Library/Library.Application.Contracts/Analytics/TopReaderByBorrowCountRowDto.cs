using Library.Application.Contracts.Readers;

namespace Library.Application.Contracts.Analytics;

/// <summary>
/// DTO для строки отчёта по читателям с количеством выдач за период
/// </summary>
/// <param name="Reader">DTO для получения Reader</param>
/// <param name="BorrowCount">Количество выдач за период</param>
public record TopReaderByBorrowCountRowDto(
    ReaderDto Reader,
    int BorrowCount
);