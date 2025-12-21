using Bogus;
using Library.Application.Contracts.BookLoans;

namespace Library.Generator.Kafka.Host.Generator;

/// <summary>
/// Генератор тестовых DTO выдач книг для отправки в Kafka
/// </summary>
public class BookLoanGenerator
{
    /// <summary>
    /// Сгенерировать список DTO для создания или обновления выдач книг
    /// </summary>
    /// <param name="count">Количество генерируемых DTO</param>
    /// <returns>Список DTO для создания или обновления выдач книг</returns>
    public static IList<BookLoanCreateUpdateDto> Generate(int count) =>
        new Faker<BookLoanCreateUpdateDto>()
            .WithRecord()
            .RuleFor(x => x.BookId, f => f.Random.Int(1, 26))
            .RuleFor(x => x.ReaderId, f => f.Random.Int(1, 20))
            .RuleFor(x => x.LoanDate, f => f.Date.Between(DateTime.UtcNow.AddYears(-1), DateTime.UtcNow))
            .RuleFor(x => x.Days, f => f.Random.Int(1, 60))
            .RuleFor(x => x.ReturnDate, (f, x) =>
            {
                var isReturned = f.Random.Bool(0.6f);
                if (!isReturned)
                    return null;

                var min = x.LoanDate;
                var max = x.LoanDate.AddDays(x.Days);

                var returnMax = max.AddDays(f.Random.Int(0, 7));

                return f.Date.Between(min, returnMax);
            })
            .Generate(count);
}