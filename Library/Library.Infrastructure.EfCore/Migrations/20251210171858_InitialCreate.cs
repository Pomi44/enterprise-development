using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "edition_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_edition_types", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "readers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    last_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    patronymic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_readers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    inventory_number = table.Column<int>(type: "int", nullable: false),
                    catalog_code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authors = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    year = table.Column<int>(type: "int", nullable: false),
                    edition_type_id = table.Column<int>(type: "int", nullable: false),
                    publisher_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                    table.ForeignKey(
                        name: "fk_books_edition_type_id",
                        column: x => x.edition_type_id,
                        principalTable: "edition_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_books_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "book_loans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    reader_id = table.Column<int>(type: "int", nullable: false),
                    loan_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    days = table.Column<int>(type: "int", nullable: false),
                    return_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_loans", x => x.id);
                    table.ForeignKey(
                        name: "fk_book_loans_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_book_loans_reader_id",
                        column: x => x.reader_id,
                        principalTable: "readers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "edition_types",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Книга" },
                    { 2, "Учебник" },
                    { 3, "Справочник" },
                    { 4, "Журнал" },
                    { 5, "Монография" },
                    { 6, "Сборник" },
                    { 7, "Методическое пособие" },
                    { 8, "Комикс" },
                    { 9, "Антология" },
                    { 10, "Повесть" }
                });

            migrationBuilder.InsertData(
                table: "publishers",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Эксмо" },
                    { 2, "АСТ" },
                    { 3, "Питер" },
                    { 4, "МИФ" },
                    { 5, "Наука" },
                    { 6, "Просвещение" },
                    { 7, "Бином" },
                    { 8, "Диалектика" },
                    { 9, "Олимп-Бизнес" },
                    { 10, "Fanbook" }
                });

            migrationBuilder.InsertData(
                table: "readers",
                columns: new[] { "id", "address", "last_name", "name", "patronymic", "phone", "registration_date" },
                values: new object[,]
                {
                    { 1, "ул. Ленина, 1", "Иванов", "Иван", "Иванович", "+7 900 000-00-01", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "ул. Мира, 5", "Петров", "Пётр", "Петрович", "+7 900 000-00-02", new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "пр. Победы, 9", "Сидоров", "Сидор", "Сидорович", "+7 900 000-00-03", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "ул. Парковая, 2", "Смирнова", "Анна", null, "+7 900 000-00-04", new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "ул. Речная, 7", "Кузнецов", "Алексей", "Олегович", "+7 900 000-00-05", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "ул. Садовая, 3", "Попова", "Елена", "Игоревна", "+7 900 000-00-06", new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "ул. Новая, 11", "Васильев", "Никита", null, "+7 900 000-00-07", new DateTime(2023, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "пер. Южный, 4", "Новикова", "Мария", "Андреевна", "+7 900 000-00-08", new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "ул. Центральная, 6", "Морозов", "Дмитрий", "Сергеевич", "+7 900 000-00-09", new DateTime(2023, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "ул. Лесная, 10", "Фёдорова", "Ольга", null, "+7 900 000-00-10", new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "id", "authors", "catalog_code", "edition_type_id", "inventory_number", "publisher_id", "title", "year" },
                values: new object[,]
                {
                    { 1, "А. С. Пушкин", "ПШК-001", 1, 1001, 1, "Руслан и Людмила", 1820 },
                    { 2, "Л. Н. Толстой", "ТЛСТ-002", 1, 1002, 2, "Война и мир", 1869 },
                    { 3, "Ф. М. Достоевский", "ДСТ-003", 1, 1003, 3, "Преступление и наказание", 1866 },
                    { 4, "М. Ю. Лермонтов", "ЛРМ-004", 1, 1004, 1, "Герой нашего времени", 1840 },
                    { 5, "И. С. Тургенев", "ТРГН-005", 1, 1005, 2, "Отцы и дети", 1862 },
                    { 6, "М. А. Булгаков", "БЛГ-006", 1, 1006, 3, "Мастер и Маргарита", 1967 },
                    { 7, "Н. В. Гоголь", "ГГЛ-007", 1, 1007, 4, "Мёртвые души", 1842 },
                    { 8, "В. В. Набоков", "НБК-008", 1, 1008, 5, "Лолита", 1955 },
                    { 9, "М. М. Пришвин", "ПРШ-009", 2, 1009, 6, "Кладовая солнца", 1945 },
                    { 10, "А. Н. и Б. Н. Стругацкие", "СТРГ-010", 1, 1010, 7, "Пикник на обочине", 1972 },
                    { 11, "А. С. Пушкин", "ПШК-001-2", 1, 1011, 1, "Руслан и Людмила", 1820 },
                    { 12, "Л. Н. Толстой", "ТЛСТ-002-2", 1, 1012, 2, "Война и мир", 1869 },
                    { 13, "М. А. Булгаков", "БЛГ-006-2", 1, 1013, 3, "Мастер и Маргарита", 1967 }
                });

            migrationBuilder.InsertData(
                table: "book_loans",
                columns: new[] { "id", "book_id", "days", "loan_date", "reader_id", "return_date" },
                values: new object[,]
                {
                    { 1, 1, 20, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 14, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 30, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null },
                    { 4, 4, 7, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, 21, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null },
                    { 6, 6, 10, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 7, 60, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null },
                    { 8, 8, 5, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2025, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 9, 14, new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null },
                    { 10, 10, 30, new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_book_loans_book_id",
                table: "book_loans",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_loans_book_id_return_date",
                table: "book_loans",
                columns: new[] { "book_id", "return_date" });

            migrationBuilder.CreateIndex(
                name: "ix_book_loans_reader_id",
                table: "book_loans",
                column: "reader_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_loans_reader_id_return_date",
                table: "book_loans",
                columns: new[] { "reader_id", "return_date" });

            migrationBuilder.CreateIndex(
                name: "ix_books_edition_type_id",
                table: "books",
                column: "edition_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_publisher_id",
                table: "books",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ux_edition_types_name",
                table: "edition_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_publishers_name",
                table: "publishers",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_loans");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "readers");

            migrationBuilder.DropTable(
                name: "edition_types");

            migrationBuilder.DropTable(
                name: "publishers");
        }
    }
}
