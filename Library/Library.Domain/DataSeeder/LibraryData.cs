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
    public static readonly List<EditionType> EditionTypes = new()
    {
        new() { Id = "et1", Name = "Книга" },
        new() { Id = "et2", Name = "Учебник" },
        new() { Id = "et3", Name = "Справочник" },
        new() { Id = "et4", Name = "Журнал" },
        new() { Id = "et5", Name = "Монография" },
        new() { Id = "et6", Name = "Сборник" },
        new() { Id = "et7", Name = "Методическое пособие" },
        new() { Id = "et8", Name = "Комикс" },
        new() { Id = "et9", Name = "Антология" },
        new() { Id = "et10", Name = "Повесть" }
    };

    /// <summary>
    /// Справочник издательств
    /// </summary>
    public static readonly List<Publisher> Publishers = new()
    {
        new() { Id = "p1", Name = "Эксмо" },
        new() { Id = "p2", Name = "АСТ" },
        new() { Id = "p3", Name = "Питер" },
        new() { Id = "p4", Name = "МИФ" },
        new() { Id = "p5", Name = "Наука" },
        new() { Id = "p6", Name = "Просвещение" },
        new() { Id = "p7", Name = "Бином" },
        new() { Id = "p8", Name = "Диалектика" },
        new() { Id = "p9", Name = "Олимп-Бизнес" },
        new() { Id = "p10", Name = "Fanbook" }
    };

    /// <summary>
    /// Авторы
    /// </summary>
    public static readonly List<Author> Authors = new()
    {
        new() { Id = "a1", LastName = "Пушкин",    Name = "Александр", Patronymic = "Сергеевич" },
        new() { Id = "a2", LastName = "Толстой",   Name = "Лев",       Patronymic = "Николаевич" },
        new() { Id = "a3", LastName = "Достоевский", Name = "Фёдор",   Patronymic = "Михайлович" },
        new() { Id = "a4", LastName = "Лермонтов", Name = "Михаил",    Patronymic = "Юрьевич" },
        new() { Id = "a5", LastName = "Тургенев",  Name = "Иван",      Patronymic = "Сергеевич" },
        new() { Id = "a6", LastName = "Булгаков",  Name = "Михаил",    Patronymic = "Афанасьевич" },
        new() { Id = "a7", LastName = "Гоголь",    Name = "Николай",   Patronymic = "Васильевич" },
        new() { Id = "a8", LastName = "Набоков",   Name = "Владимир",  Patronymic = "Владимирович" },
        new() { Id = "a9", LastName = "Пришвин",   Name = "Михаил",    Patronymic = "Михайлович" },
        new() { Id = "a10", LastName = "Стругацкий", Name = "Аркадий", Patronymic = "Натанович" }
    };

    /// <summary>
    /// Читатели
    /// </summary>
    public static readonly List<Reader> Readers = new()
    {
        new() { Id = "r1", LastName = "Иванов",   Name = "Иван",    Patronymic = "Иванович", Address = "ул. Ленина, 1",  Phone = "+7 900 000-00-01", RegistrationDate = new DateTime(2023, 1, 10) },
        new() { Id = "r2", LastName = "Петров",   Name = "Пётр",    Patronymic = "Петрович", Address = "ул. Мира, 5",     Phone = "+7 900 000-00-02", RegistrationDate = new DateTime(2023, 2,  1) },
        new() { Id = "r3", LastName = "Сидоров",  Name = "Сидор",   Patronymic = "Сидорович",Address = "пр. Победы, 9",  Phone = "+7 900 000-00-03", RegistrationDate = new DateTime(2023, 3, 15) },
        new() { Id = "r4", LastName = "Смирнова", Name = "Анна",    Patronymic = null,       Address = "ул. Парковая, 2",Phone = "+7 900 000-00-04", RegistrationDate = new DateTime(2023, 4, 20) },
        new() { Id = "r5", LastName = "Кузнецов", Name = "Алексей", Patronymic = "Олегович", Address = "ул. Речная, 7",  Phone = "+7 900 000-00-05", RegistrationDate = new DateTime(2023, 5,  5) },
        new() { Id = "r6", LastName = "Попова",   Name = "Елена",   Patronymic = "Игоревна", Address = "ул. Садовая, 3", Phone = "+7 900 000-00-06", RegistrationDate = new DateTime(2023, 6, 12) },
        new() { Id = "r7", LastName = "Васильев", Name = "Никита",  Patronymic = null,       Address = "ул. Новая, 11",  Phone = "+7 900 000-00-07", RegistrationDate = new DateTime(2023, 7,  8) },
        new() { Id = "r8", LastName = "Новикова", Name = "Мария",   Patronymic = "Андреевна",Address = "пер. Южный, 4",  Phone = "+7 900 000-00-08", RegistrationDate = new DateTime(2023, 8, 25) },
        new() { Id = "r9", LastName = "Морозов",  Name = "Дмитрий", Patronymic = "Сергеевич",Address = "ул. Центральная, 6",Phone = "+7 900 000-00-09",RegistrationDate = new DateTime(2023, 9, 14) },
        new() { Id = "r10", LastName = "Фёдорова", Name = "Ольга",  Patronymic = null,       Address = "ул. Лесная, 10", Phone = "+7 900 000-00-10", RegistrationDate = new DateTime(2023,10,  2) }
    };

    /// <summary>
    /// Каталог книг
    /// </summary>
    public static readonly List<Book> Books = new()
    {
        new() { Id = "b1",  InventoryNumber = 1001, CatalogCode = "ПШК-001",   Title = "Руслан и Людмила", Year = 1820, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p1", Publisher = Publishers[0], Authors = new(){ Authors[0] } },
        new() { Id = "b2",  InventoryNumber = 1002, CatalogCode = "ТЛСТ-002",  Title = "Война и мир", Year = 1869, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p2", Publisher = Publishers[1], Authors = new(){ Authors[1] } },
        new() { Id = "b3",  InventoryNumber = 1003, CatalogCode = "ДСТ-003",   Title = "Преступление и наказание", Year = 1866, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p3", Publisher = Publishers[2], Authors = new(){ Authors[2] } },
        new() { Id = "b4",  InventoryNumber = 1004, CatalogCode = "ЛРМ-004",   Title = "Герой нашего времени", Year = 1840, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p1", Publisher = Publishers[0], Authors = new(){ Authors[3] } },
        new() { Id = "b5",  InventoryNumber = 1005, CatalogCode = "ТРГН-005",  Title = "Отцы и дети", Year = 1862, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p2", Publisher = Publishers[1], Authors = new(){ Authors[4] } },
        new() { Id = "b6",  InventoryNumber = 1006, CatalogCode = "БЛГ-006",   Title = "Мастер и Маргарита", Year = 1967, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p3", Publisher = Publishers[2], Authors = new(){ Authors[5] } },
        new() { Id = "b7",  InventoryNumber = 1007, CatalogCode = "ГГЛ-007",   Title = "Мёртвые души", Year = 1842, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p4", Publisher = Publishers[3], Authors = new(){ Authors[6] } },
        new() { Id = "b8",  InventoryNumber = 1008, CatalogCode = "НБК-008",   Title = "Лолита", Year = 1955, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p5", Publisher = Publishers[4], Authors = new(){ Authors[7] } },
        new() { Id = "b9",  InventoryNumber = 1009, CatalogCode = "ПРШ-009",   Title = "Кладовая солнца", Year = 1945, EditionTypeId = "et2", EditionType = EditionTypes[1], PublisherId = "p6", Publisher = Publishers[5], Authors = new(){ Authors[8] } },
        new() { Id = "b10", InventoryNumber = 1010, CatalogCode = "СТРГ-010",  Title = "Пикник на обочине", Year = 1972, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p7", Publisher = Publishers[6], Authors = new(){ Authors[9] } },
        new() { Id = "b11", InventoryNumber = 1011, CatalogCode = "ПШК-001-2", Title = "Руслан и Людмила", Year = 1820, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p1", Publisher = Publishers[0], Authors = new(){ Authors[0] } },
        new() { Id = "b12", InventoryNumber = 1012, CatalogCode = "ТЛСТ-002-2",Title = "Война и мир", Year = 1869, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p2", Publisher = Publishers[1], Authors = new(){ Authors[1] } },
        new() { Id = "b13", InventoryNumber = 1013, CatalogCode = "БЛГ-006-2", Title = "Мастер и Маргарита", Year = 1967, EditionTypeId = "et1", EditionType = EditionTypes[0], PublisherId = "p3", Publisher = Publishers[2], Authors = new(){ Authors[5] } }
    };

    /// <summary>
    /// Выдачи книг читателям 
    /// </summary>
    public static readonly List<BookLoan> BookLoans = new()
    {
        new() { Id = "bl1",  BookId = "b1",  Book = Books[0],  Reader = Readers[0], LoanDate = new DateTime(2024,12,20), Days = 20, ReturnDate = new DateTime(2025,1,10) },
        new() { Id = "bl2",  BookId = "b2",  Book = Books[1],  Reader = Readers[1], LoanDate = new DateTime(2025,1, 5), Days = 14, ReturnDate = new DateTime(2025,1,19) },
        new() { Id = "bl3",  BookId = "b3",  Book = Books[2],  Reader = Readers[2], LoanDate = new DateTime(2025,2,10), Days = 30, ReturnDate = null },
        new() { Id = "bl4",  BookId = "b4",  Book = Books[3],  Reader = Readers[3], LoanDate = new DateTime(2025,3, 1), Days =  7, ReturnDate = new DateTime(2025,3, 8) },
        new() { Id = "bl5",  BookId = "b5",  Book = Books[4],  Reader = Readers[4], LoanDate = new DateTime(2025,3,15), Days = 21, ReturnDate = null },
        new() { Id = "bl6",  BookId = "b6",  Book = Books[5],  Reader = Readers[5], LoanDate = new DateTime(2025,4, 2), Days = 10, ReturnDate = new DateTime(2025,4,12) },
        new() { Id = "bl7",  BookId = "b7",  Book = Books[6],  Reader = Readers[6], LoanDate = new DateTime(2025,5,10), Days = 60, ReturnDate = null },
        new() { Id = "bl8",  BookId = "b8",  Book = Books[7],  Reader = Readers[7], LoanDate = new DateTime(2025,6, 1), Days =  5, ReturnDate = new DateTime(2025,6, 6) },
        new() { Id = "bl9",  BookId = "b9",  Book = Books[8],  Reader = Readers[8], LoanDate = new DateTime(2025,7,20), Days = 14, ReturnDate = null },
        new() { Id = "bl10", BookId = "b10", Book = Books[9],  Reader = Readers[9], LoanDate = new DateTime(2025,8,10), Days = 30, ReturnDate = new DateTime(2025,9, 9) }
    };
}
