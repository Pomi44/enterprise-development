namespace Library.Application.Contracts.EditionTypes;

/// <summary>
/// DTO для создания или обновления EditionType
/// </summary>
/// <param name="Name">Наименование вида издания</param>
public record EditionTypeCreateUpdateDto(
    string Name
);