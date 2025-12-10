namespace Library.Domain.Models;

/// <summary>
/// Выдача книги читателю
/// </summary>
public class BookLoan
{
    /// <summary>
    /// Уникальный идентификатор записи выдачи
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор книги
    /// </summary>
    public required int BookId { get; set; }

    /// <summary>
    /// Книга, которая выдана
    /// </summary>
    public Book? Book { get; set; }

    /// <summary>
    /// Идентификатор читателя
    /// </summary>
    public required int ReaderId { get; set; }

    /// <summary>
    /// Читатель, которому выдана книга
    /// </summary>
    public Reader? Reader { get; set; }

    /// <summary>
    /// Дата выдачи книги
    /// </summary>
    public DateTime LoanDate { get; set; }

    /// <summary>
    /// Количество дней, на которое выдана книга
    /// </summary>
    public required int Days { get; set; }

    /// <summary>
    /// Предположительная дата возврата книги
    /// </summary>
    public DateTime DueDate => LoanDate.AddDays(Days);

    /// <summary>
    /// Фактическая дата возврата книги (null, если ещё не сдана)
    /// </summary>
    public DateTime? ReturnDate { get; set; }
}