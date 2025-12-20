using Confluent.Kafka;

namespace Library.Generator.Kafka.Host.Serializers;

/// <summary>
/// Сериализатор Kafka ключа Guid в бинарное представление
/// </summary>
public sealed class GuidKeySerializer : ISerializer<Guid>
{
    /// <summary>
    /// Сериализовать ключ Guid в массив байт
    /// </summary>
    /// <param name="data">Значение ключа</param>
    /// <param name="context">Контекст сериализации</param>
    /// <returns>Бинарное представление Guid</returns>
    public byte[] Serialize(Guid data, SerializationContext context)
        => data.ToByteArray();
}
