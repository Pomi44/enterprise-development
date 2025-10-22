namespace Library.Domain.Models;

/// <summary>
/// Автор книги
/// </summary>
public class Author
{
    /// <summary>
    /// Уникальный идентификатор автора
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Фамилия автора
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя автора
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Отчество автора
    /// </summary>
    public string? Patronymic { get; set; }
}