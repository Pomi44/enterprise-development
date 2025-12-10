namespace Library.Application.Contracts.Books;

/// <summary>
/// DTO для создания или обновления Book
/// </summary>
/// <param name="InventoryNumber">Инвентарный номер</param>
/// <param name="CatalogCode">Шифр в алфавитном каталоге</param>
/// <param name="Title">Название книги</param>
/// <param name="Authors">Инициалы и фамилии авторов</param>
/// <param name="Year">Год издания</param>
/// <param name="EditionTypeId">Идентификатор вида издания</param>
/// <param name="PublisherId">Идентификатор издательства</param>
public record BookCreateUpdateDto(
    int InventoryNumber,
    string CatalogCode,
    string Title,
    string? Authors,
    int Year,
    int EditionTypeId,
    int PublisherId
);