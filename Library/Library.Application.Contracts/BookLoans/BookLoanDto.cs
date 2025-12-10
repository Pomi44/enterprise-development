namespace Library.Application.Contracts.BookLoans;

/// <summary>
/// DTO для получения BookLoan
/// </summary>
/// <param name="Id">Уникальный идентификатор записи выдачи</param>
/// <param name="BookId">Идентификатор книги</param>
/// <param name="ReaderId">Идентификатор читателя</param>
/// <param name="LoanDate">Дата выдачи книги</param>
/// <param name="Days">Количество дней, на которое выдана книга</param>
/// <param name="ReturnDate">Фактическая дата возврата книги</param>
public record BookLoanDto(
    int Id,
    int BookId,
    int ReaderId,
    DateTime LoanDate,
    int Days,
    DateTime? ReturnDate
);