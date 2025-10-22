namespace Library.Domain.Models;

/// <summary>
/// Вид издания
/// </summary>
public class EditionType
{
    /// <summary>
    /// Уникальный идентификатор вида издания
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Наименование вида издания
    /// </summary>
    public required string Name { get; set; }
}