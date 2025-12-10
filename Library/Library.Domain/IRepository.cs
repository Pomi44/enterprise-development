namespace Library.Domain;

/// <summary>
/// Базовый контракт репозитория для CRUD-операций над сущностями
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип ключа сущности</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Создать сущность и вернуть сохранённый экземпляр
    /// </summary>
    /// <param name="entity">Сущность для создания</param>
    /// <returns>Созданная сущность</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Прочитать сущность по идентификатору или вернуть null если сущность не найдена
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>Найденная сущность или null</returns>
    public Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Прочитать все сущности указанного типа
    /// </summary>
    /// <returns>Список сущностей</returns>
    public Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Обновить сущность и вернуть обновлённый экземпляр
    /// </summary>
    /// <param name="entity">Сущность для обновления</param>
    /// <returns>Обновлённая сущность</returns>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удалить сущность по идентификатору и вернуть признак успешного удаления
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>true если сущность удалена иначе false</returns>
    public Task<bool> Delete(TKey entityId);
}