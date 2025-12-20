using Confluent.Kafka;

namespace Library.Infrastructure.Kafka.Deserializers;

/// <summary>
/// Десериализатор Kafka ключа Guid из бинарного представления
/// </summary>
public class GuidKeyDeserializer : IDeserializer<Guid>
{
    /// <summary>
    /// Десериализовать ключ сообщения в Guid
    /// </summary>
    /// <param name="data">Сырые байты ключа</param>
    /// <param name="isNull">Признак отсутствия ключа</param>
    /// <param name="context">Контекст десериализации</param>
    /// <returns>Десериализованный Guid ключ</returns>
    public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull || data.IsEmpty)
            return Guid.Empty;

        if (data.Length != 16)
            throw new InvalidOperationException($"Invalid Guid key length={data.Length} expected 16");

        return new Guid(data);
    }
}