using Library.Domain.Models;

namespace Library.Domain.DataSeeder;

/// <summary>
/// Заполнение БД
/// </summary>
public class LibraryData
{
    /// <summary>
    /// Справочник видов издания.
    /// </summary>
    public static readonly List<EditionType> EditionTypes =
    [
        new EditionType { Id = 1, Name = "Книга" },
        new EditionType { Id = 2, Name = "Учебник" },
        new EditionType { Id = 3, Name = "Справочник" },
        new EditionType { Id = 4, Name = "Журнал" },
        new EditionType { Id = 5, Name = "Монография" },
        new EditionType { Id = 6, Name = "Сборник" },
        new EditionType { Id = 7, Name = "Методическое пособие" },
        new EditionType { Id = 8, Name = "Комикс" },
        new EditionType { Id = 9, Name = "Антология" },
        new EditionType { Id = 10, Name = "Повесть" }
    ];

    /// <summary>
    /// Справочник издательств.
    /// </summary>
    public static readonly List<Publisher> Publishers =
    [
        new Publisher { Id = 1, Name = "Эксмо" },
        new Publisher { Id = 2, Name = "АСТ" },
        new Publisher { Id = 3, Name = "Питер" },
        new Publisher { Id = 4, Name = "МИФ" },
        new Publisher { Id = 5, Name = "Наука" },
        new Publisher { Id = 6, Name = "Просвещение" },
        new Publisher { Id = 7, Name = "Бином" },
        new Publisher { Id = 8, Name = "Диалектика" },
        new Publisher { Id = 9, Name = "Олимп-Бизнес" },
        new Publisher { Id = 10, Name = "Fanbook" }
    ];

    /// <summary>
    /// Читатели.
    /// </summary>
    public static readonly List<Reader> Readers =
    [
        new Reader { Id = 1, LastName = "Иванов", Name = "Иван", Patronymic = "Иванович", Address = "ул. Ленина, 1", Phone = "+7 900 000-00-01", RegistrationDate = new DateTime(2023, 1, 10) },
        new Reader { Id = 2, LastName = "Петров", Name = "Пётр", Patronymic = "Петрович", Address = "ул. Мира, 5", Phone = "+7 900 000-00-02", RegistrationDate = new DateTime(2023, 2, 1) },
        new Reader { Id = 3, LastName = "Сидоров", Name = "Сидор", Patronymic = "Сидорович", Address = "пр. Победы, 9", Phone = "+7 900 000-00-03", RegistrationDate = new DateTime(2023, 3, 15) },
        new Reader { Id = 4, LastName = "Смирнова", Name = "Анна", Patronymic = null, Address = "ул. Парковая, 2", Phone = "+7 900 000-00-04", RegistrationDate = new DateTime(2023, 4, 20) },
        new Reader { Id = 5, LastName = "Кузнецов", Name = "Алексей", Patronymic = "Олегович", Address = "ул. Речная, 7", Phone = "+7 900 000-00-05", RegistrationDate = new DateTime(2023, 5, 5) },
        new Reader { Id = 6, LastName = "Попова", Name = "Елена", Patronymic = "Игоревна", Address = "ул. Садовая, 3", Phone = "+7 900 000-00-06", RegistrationDate = new DateTime(2023, 6, 12) },
        new Reader { Id = 7, LastName = "Васильев", Name = "Никита", Patronymic = null, Address = "ул. Новая, 11", Phone = "+7 900 000-00-07", RegistrationDate = new DateTime(2023, 7, 8) },
        new Reader { Id = 8, LastName = "Новикова", Name = "Мария", Patronymic = "Андреевна", Address = "пер. Южный, 4", Phone = "+7 900 000-00-08", RegistrationDate = new DateTime(2023, 8, 25) },
        new Reader { Id = 9, LastName = "Морозов", Name = "Дмитрий", Patronymic = "Сергеевич", Address = "ул. Центральная, 6", Phone = "+7 900 000-00-09", RegistrationDate = new DateTime(2023, 9, 14) },
        new Reader { Id = 10, LastName = "Фёдорова", Name = "Ольга", Patronymic = null, Address = "ул. Лесная, 10", Phone = "+7 900 000-00-10", RegistrationDate = new DateTime(2023, 10, 2) }
    ];

    /// <summary>
    /// Каталог книг.
    /// </summary>
    public static readonly List<Book> Books =
    [
        new Book { Id = 1, InventoryNumber = 1001, CatalogCode = "ПШК-001", Title = "Руслан и Людмила", Authors = "А. С. Пушкин", Year = 1820, EditionTypeId = 1, PublisherId = 1 },
        new Book { Id = 2, InventoryNumber = 1002, CatalogCode = "ТЛСТ-002", Title = "Война и мир", Authors = "Л. Н. Толстой", Year = 1869, EditionTypeId = 1, PublisherId = 2 },
        new Book { Id = 3, InventoryNumber = 1003, CatalogCode = "ДСТ-003", Title = "Преступление и наказание", Authors = "Ф. М. Достоевский", Year = 1866, EditionTypeId = 1, PublisherId = 3 },
        new Book { Id = 4, InventoryNumber = 1004, CatalogCode = "ЛРМ-004", Title = "Герой нашего времени", Authors = "М. Ю. Лермонтов", Year = 1840, EditionTypeId = 1, PublisherId = 1 },
        new Book { Id = 5, InventoryNumber = 1005, CatalogCode = "ТРГН-005", Title = "Отцы и дети", Authors = "И. С. Тургенев", Year = 1862, EditionTypeId = 1, PublisherId = 2 },
        new Book { Id = 6, InventoryNumber = 1006, CatalogCode = "БЛГ-006", Title = "Мастер и Маргарита", Authors = "М. А. Булгаков", Year = 1967, EditionTypeId = 1, PublisherId = 3 },
        new Book { Id = 7, InventoryNumber = 1007, CatalogCode = "ГГЛ-007", Title = "Мёртвые души", Authors = "Н. В. Гоголь", Year = 1842, EditionTypeId = 1, PublisherId = 4 },
        new Book { Id = 8, InventoryNumber = 1008, CatalogCode = "НБК-008", Title = "Лолита", Authors = "В. В. Набоков", Year = 1955, EditionTypeId = 1, PublisherId = 5 },
        new Book { Id = 9, InventoryNumber = 1009, CatalogCode = "ПРШ-009", Title = "Кладовая солнца", Authors = "М. М. Пришвин", Year = 1945, EditionTypeId = 2, PublisherId = 6 },
        new Book { Id = 10, InventoryNumber = 1010, CatalogCode = "СТРГ-010", Title = "Пикник на обочине", Authors = "А. Н. и Б. Н. Стругацкие", Year = 1972, EditionTypeId = 1, PublisherId = 7 },
        new Book { Id = 11, InventoryNumber = 1011, CatalogCode = "ПШК-001-2", Title = "Руслан и Людмила", Authors = "А. С. Пушкин", Year = 1820, EditionTypeId = 1, PublisherId = 1 },
        new Book { Id = 12, InventoryNumber = 1012, CatalogCode = "ТЛСТ-002-2", Title = "Война и мир", Authors = "Л. Н. Толстой", Year = 1869, EditionTypeId = 1, PublisherId = 2 },
        new Book { Id = 13, InventoryNumber = 1013, CatalogCode = "БЛГ-006-2", Title = "Мастер и Маргарита", Authors = "М. А. Булгаков", Year = 1967, EditionTypeId = 1, PublisherId = 3 }
    ];

    /// <summary>
    /// Выдачи книг читателям.
    /// </summary>
    public static readonly List<BookLoan> BookLoans =
    [
        new BookLoan { Id = 1,  BookId = 1,  ReaderId = 1,  LoanDate = new DateTime(2024, 12, 20), Days = 20, ReturnDate = new DateTime(2025,  1, 10) },
        new BookLoan { Id = 2,  BookId = 2,  ReaderId = 2,  LoanDate = new DateTime(2025,  1,  5), Days = 14, ReturnDate = new DateTime(2025,  1, 19) },
        new BookLoan { Id = 3,  BookId = 3,  ReaderId = 3,  LoanDate = new DateTime(2025,  2, 10), Days = 30, ReturnDate = null },
        new BookLoan { Id = 4,  BookId = 4,  ReaderId = 4,  LoanDate = new DateTime(2025,  3,  1), Days =  7, ReturnDate = new DateTime(2025,  3,  8) },
        new BookLoan { Id = 5,  BookId = 5,  ReaderId = 5,  LoanDate = new DateTime(2025,  3, 15), Days = 21, ReturnDate = null },
        new BookLoan { Id = 6,  BookId = 6,  ReaderId = 6,  LoanDate = new DateTime(2025,  4,  2), Days = 10, ReturnDate = new DateTime(2025,  4, 12) },
        new BookLoan { Id = 7,  BookId = 7,  ReaderId = 7,  LoanDate = new DateTime(2025,  5, 10), Days = 60, ReturnDate = null },
        new BookLoan { Id = 8,  BookId = 8,  ReaderId = 8,  LoanDate = new DateTime(2025,  6,  1), Days =  5, ReturnDate = new DateTime(2025,  6,  6) },
        new BookLoan { Id = 9,  BookId = 9,  ReaderId = 9,  LoanDate = new DateTime(2025,  7, 20), Days = 14, ReturnDate = null },
        new BookLoan { Id = 10, BookId = 10, ReaderId = 10, LoanDate = new DateTime(2025,  8, 10), Days = 30, ReturnDate = new DateTime(2025,  9,  9) }
    ];
}