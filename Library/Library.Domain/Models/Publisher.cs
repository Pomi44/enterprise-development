namespace Library.Domain.Models;

/// <summary>
/// Издательство
/// </summary>
public class Publisher
{
    /// <summary>
    /// Уникальный идентификатор издательства
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Название издательства
    /// </summary>
    public required string Name { get; set; }
}
