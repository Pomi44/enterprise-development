using Confluent.Kafka;
using Library.Application.Contracts.BookLoans;
using System.Text.Json;

namespace Library.Generator.Kafka.Host.Serializers;

/// <summary>
/// JSON сериализатор Kafka значения для списка DTO выдач книг
/// </summary>
public sealed class BookLoanValueSerializer : ISerializer<IList<BookLoanCreateUpdateDto>>
{
    /// <summary>
    /// Сериализовать список DTO выдач книг в JSON массив байт
    /// </summary>
    /// <param name="data">Список DTO выдач книг</param>
    /// <param name="context">Контекст сериализации</param>
    /// <returns>JSON в UTF-8</returns>
    public byte[] Serialize(IList<BookLoanCreateUpdateDto> data, SerializationContext context)
        => JsonSerializer.SerializeToUtf8Bytes(data);
}