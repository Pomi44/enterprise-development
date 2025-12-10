using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных библиотеки для EF Core
/// </summary>
public class LibraryDbContext(DbContextOptions contextOptions) : DbContext(contextOptions)
{
    /// <summary>
    /// Таблица книг
    /// </summary>
    public DbSet<Book> Books { get; set; } = null!;

    /// <summary>
    /// Таблица выдач книг читателям
    /// </summary>
    public DbSet<BookLoan> BookLoans { get; set; } = null!;

    /// <summary>
    /// Таблица видов изданий
    /// </summary>
    public DbSet<EditionType> EditionTypes { get; set; } = null!;

    /// <summary>
    /// Таблица издательств
    /// </summary>
    public DbSet<Publisher> Publishers { get; set; } = null!;

    /// <summary>
    /// Таблица читателей
    /// </summary>
    public DbSet<Reader> Readers { get; set; } = null!;

    /// <summary>
    /// Конфигурация схемы базы данных и сопоставление сущностей с таблицами MySQL
    /// </summary>
    /// <param name="modelBuilder">Построитель модели EF Core</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reader>(e =>
        {
            e.ToTable("readers");

            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");

            e.Property(x => x.LastName).HasColumnName("last_name").IsRequired();
            e.Property(x => x.Name).HasColumnName("name").IsRequired();
            e.Property(x => x.Patronymic).HasColumnName("patronymic").IsRequired(false);
            e.Property(x => x.Address).HasColumnName("address").IsRequired();
            e.Property(x => x.Phone).HasColumnName("phone").IsRequired();

            e.Property(x => x.RegistrationDate)
                .HasColumnName("registration_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Publisher>(e =>
        {
            e.ToTable("publishers");

            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");

            e.Property(x => x.Name).HasColumnName("name").IsRequired();

            e.HasIndex(x => x.Name)
                .HasDatabaseName("ux_publishers_name")
                .IsUnique();
        });

        modelBuilder.Entity<EditionType>(e =>
        {
            e.ToTable("edition_types");

            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");

            e.Property(x => x.Name).HasColumnName("name").IsRequired();

            e.HasIndex(x => x.Name)
                .HasDatabaseName("ux_edition_types_name")
                .IsUnique();
        });

        modelBuilder.Entity<Book>(e =>
        {
            e.ToTable("books");

            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");

            e.Property(x => x.InventoryNumber).HasColumnName("inventory_number").IsRequired();
            e.Property(x => x.CatalogCode).HasColumnName("catalog_code").IsRequired();
            e.Property(x => x.Title).HasColumnName("title").IsRequired();
            e.Property(x => x.Authors).HasColumnName("authors").IsRequired(false);
            e.Property(x => x.Year).HasColumnName("year").IsRequired();

            e.Property(x => x.EditionTypeId).HasColumnName("edition_type_id").IsRequired();
            e.Property(x => x.PublisherId).HasColumnName("publisher_id").IsRequired();

            e.HasIndex(x => x.EditionTypeId)
                .HasDatabaseName("ix_books_edition_type_id");

            e.HasIndex(x => x.PublisherId)
                .HasDatabaseName("ix_books_publisher_id");

            e.HasOne(x => x.EditionType)
                .WithMany()
                .HasForeignKey(x => x.EditionTypeId)
                .HasConstraintName("fk_books_edition_type_id")
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Publisher)
                .WithMany()
                .HasForeignKey(x => x.PublisherId)
                .HasConstraintName("fk_books_publisher_id")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BookLoan>(e =>
        {
            e.ToTable("book_loans");

            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id");

            e.Property(x => x.BookId).HasColumnName("book_id").IsRequired();
            e.Property(x => x.ReaderId).HasColumnName("reader_id").IsRequired();

            e.Property(x => x.LoanDate)
                .HasColumnName("loan_date")
                .HasColumnType("datetime")
                .IsRequired();

            e.Property(x => x.Days).HasColumnName("days").IsRequired();

            e.Property(x => x.ReturnDate)
                .HasColumnName("return_date")
                .HasColumnType("datetime")
                .IsRequired(false);

            e.Ignore(x => x.DueDate);

            e.HasIndex(x => x.BookId)
                .HasDatabaseName("ix_book_loans_book_id");

            e.HasIndex(x => x.ReaderId)
                .HasDatabaseName("ix_book_loans_reader_id");

            e.HasIndex(x => new { x.BookId, x.ReturnDate })
                .HasDatabaseName("ix_book_loans_book_id_return_date");

            e.HasIndex(x => new { x.ReaderId, x.ReturnDate })
                .HasDatabaseName("ix_book_loans_reader_id_return_date");

            e.HasOne(x => x.Book)
                .WithMany()
                .HasForeignKey(x => x.BookId)
                .HasConstraintName("fk_book_loans_book_id")
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Reader)
                .WithMany()
                .HasForeignKey(x => x.ReaderId)
                .HasConstraintName("fk_book_loans_reader_id")
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}