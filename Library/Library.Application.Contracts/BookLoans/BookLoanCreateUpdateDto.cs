namespace Library.Application.Contracts.BookLoans;

/// <summary>
/// DTO для создания или обновления BookLoan
/// </summary>
/// <param name="BookId">Идентификатор книги</param>
/// <param name="ReaderId">Идентификатор читателя</param>
/// <param name="LoanDate">Дата выдачи книги</param>
/// <param name="Days">Количество дней, на которое выдана книга</param>
/// <param name="ReturnDate">Фактическая дата возврата книги</param>
public record BookLoanCreateUpdateDto(
    int BookId,
    int ReaderId,
    DateTime LoanDate,
    int Days,
    DateTime? ReturnDate
);