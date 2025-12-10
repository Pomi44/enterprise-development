namespace Library.Application.Contracts;

/// <summary>
/// Контракт прикладного сервиса для выполнения CRUD-операций над DTO
/// </summary>
/// <typeparam name="TDto">Тип DTO для получения сущности</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания или обновления сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создать сущность на основе DTO и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для создания</param>
    /// <returns>DTO созданной сущности</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получить DTO по идентификатору или вернуть null если сущность не найдена
    /// </summary>
    /// <param name="dtoId">Идентификатор сущности</param>
    /// <returns>DTO найденной сущности или null</returns>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Получить список всех DTO
    /// </summary>
    /// <returns>Список DTO</returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Обновить сущность по идентификатору на основе DTO и вернуть DTO результата
    /// </summary>
    /// <param name="dto">DTO для обновления</param>
    /// <param name="dtoId">Идентификатор обновляемой сущности</param>
    /// <returns>DTO обновлённой сущности</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор сущности</param>
    /// <returns>true если сущность удалена иначе false</returns>
    public Task<bool> Delete(TKey dtoId);
}