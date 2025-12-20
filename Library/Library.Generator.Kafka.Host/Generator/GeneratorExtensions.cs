using Bogus;
using System.Runtime.CompilerServices;

namespace Library.Generator.Kafka.Host.Generator;

/// <summary>
/// Расширения Bogus для генерации record типов без вызова конструктора
/// </summary>
public static class GeneratorExtensions
{
    /// <summary>
    /// Настройка Faker на создание экземпляров record через неинициализированный объект
    /// </summary>
    /// <typeparam name="T">Тип сущности для генерации</typeparam>
    /// <param name="faker">Экземпляр Faker</param>
    /// <returns>Faker с настроенным способом создания объектов</returns>
    public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class =>
        faker.CustomInstantiator(_ => (T)RuntimeHelpers.GetUninitializedObject(typeof(T)));

}