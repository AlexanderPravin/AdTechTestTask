using Domain.Exceptions;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected List<T> Entities;
    protected readonly IFileHandler FileHandler;
    
    public BaseRepository(IFileHandler fileHandler)
    {
        FileHandler = fileHandler;

        var entitiesJson = fileHandler.GetText().Result;

        Entities = (string.IsNullOrWhiteSpace(entitiesJson) ? [] : JsonSerializer.Deserialize<List<T>>(entitiesJson)) 
                   ?? throw new JsonException("Error deserializing entities");
    }

    public virtual void AddEntity(T entity)
    {
        entity.Id = Entities.Count == 0 ? 1 : Entities.Max(p => p.Id) + 1;

        Entities.Add(entity);
    }

    public virtual void DeleteEntity(int id)
    {
        var entity = Entities.FirstOrDefault(p => p.Id == id)
                     ?? throw new NullEntityException($"Entity with type: {typeof(T).Name}, with id: {id} not found");

        Entities.Remove(entity);
    }

    public virtual void UpdateEntity(T entity)
    {
        var index = Entities.FindIndex(p => p.Id == entity.Id);
        if (index == -1)
            throw new NullEntityException($"Entity with type: {typeof(T).Name}, with id: {entity.Id} not found");

        Entities[index] = entity;
    }

    public virtual IEnumerable<T> GetAllEntities() => Entities;

    public virtual T GetEntityById(int id) => Entities.FirstOrDefault(p => p.Id == id)
                ?? throw new NullEntityException($"Entity with type: {typeof(T).Name}, with id: {id} not found");


    public virtual async Task SaveChangesAsync() =>
        await FileHandler.SetText(JsonSerializer.Serialize(Entities));
}