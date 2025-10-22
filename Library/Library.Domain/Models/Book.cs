namespace Library.Domain.Models;

/// <summary>
/// Книга в каталоге библиотеки
/// </summary>
public class Book
{
    /// <summary>
    /// Уникальный идентификатор книги
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Инвентарный номер
    /// </summary>
    public required int InventoryNumber { get; set; }

    /// <summary>
    /// Шифр в алфавитном каталоге
    /// </summary>
    public required string CatalogCode { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Год издания
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Идентификатор вида издания
    /// </summary>
    public required string EditionTypeId { get; set; }

    /// <summary>
    /// Вид издания (навигационное свойство)
    /// </summary>
    public required EditionType EditionType { get; set; }

    /// <summary>
    /// Идентификатор издательства
    /// </summary>
    public required string PublisherId { get; set; }

    /// <summary>
    /// Издательство (навигационное свойство)
    /// </summary>
    public Publisher? Publisher { get; set; }

    /// <summary>
    /// Список авторов книги
    /// </summary>
    public List<Author> Authors { get; set; } = new();
}

