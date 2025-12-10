namespace Library.Application.Contracts.Books;

/// <summary>
/// DTO для получения Book
/// </summary>
/// <param name="Id">Уникальный идентификатор книги</param>
/// <param name="InventoryNumber">Инвентарный номер</param>
/// <param name="CatalogCode">Шифр в алфавитном каталоге</param>
/// <param name="Title">Название книги</param>
/// <param name="Authors">Инициалы и фамилии авторов</param>
/// <param name="Year">Год издания</param>
public record BookDto(
    int Id,
    int InventoryNumber,
    string CatalogCode,
    string Title,
    string? Authors,
    int Year
);