using Confluent.Kafka;
using Library.Application.Contracts.BookLoans;
using System.Text.Json;

namespace Library.Infrastructure.Kafka.Deserializers;

/// <summary>
/// JSON десериализатор Kafka сообщений для списка DTO выдач книг
/// </summary>
public class BookLoanValueDeserializer : IDeserializer<IList<BookLoanCreateUpdateDto>>
{
    /// <summary>
    /// Десериализовать список DTO выдач книг из массива байт
    /// </summary>
    /// <param name="data">байты значения Kafka сообщения</param>
    /// <param name="isNull">Признак отсутствия значения</param>
    /// <param name="context">Контекст десериализации</param>
    /// <returns>Список DTO выдач книг</returns>
    public IList<BookLoanCreateUpdateDto> Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull || data.IsEmpty)
            return [];

        return JsonSerializer.Deserialize<IList<BookLoanCreateUpdateDto>>(data) ?? [];
    }
}