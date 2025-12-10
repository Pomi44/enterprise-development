namespace Library.Application.Contracts.Readers;

/// <summary>
/// DTO для получения Reader
/// </summary>
/// <param name="Id">Уникальный идентификатор читателя</param>
/// <param name="LastName">Фамилия читателя</param>
/// <param name="Name">Имя читателя</param>
/// <param name="Patronymic">Отчество читателя</param>
/// <param name="Address">Адрес читателя</param>
/// <param name="Phone">Телефон читателя</param>
/// <param name="RegistrationDate">Дата регистрации</param>
public record ReaderDto(
    int Id,
    string LastName,
    string Name,
    string? Patronymic,
    string Address,
    string Phone,
    DateTime RegistrationDate
);