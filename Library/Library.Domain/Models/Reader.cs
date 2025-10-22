namespace Library.Domain.Models;

/// <summary>
/// Читатель библиотеки
/// </summary>
public class Reader
{
    /// <summary>
    /// Уникальный идентификатор читателя
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Фамилия читателя
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя читателя
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Отчество читателя
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Адрес читателя
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Телефон читателя
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime RegistrationDate { get; set; }
}
