namespace Library.Application.Contracts.Publishers;

/// <summary>
/// DTO для получения Publisher
/// </summary>
/// <param name="Id">Уникальный идентификатор издательства</param>
/// <param name="Name">Название издательства</param>
public record PublisherDto(
    int Id,
    string Name
);