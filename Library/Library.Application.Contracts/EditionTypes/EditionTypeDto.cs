namespace Library.Application.Contracts.EditionTypes;

/// <summary>
/// DTO для получения EditionType
/// </summary>
/// <param name="Id">Уникальный идентификатор вида издания</param>
/// <param name="Name">Наименование вида издания</param>
public record EditionTypeDto(
    int Id,
    string Name
);