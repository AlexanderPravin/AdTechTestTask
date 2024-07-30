namespace Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    void AddEntity(T entity);

    void DeleteEntity(int id);

    void UpdateEntity(T entity);

    IEnumerable<T> GetAllEntities();

    T GetEntityById(int id);

    Task SaveChangesAsync();
}