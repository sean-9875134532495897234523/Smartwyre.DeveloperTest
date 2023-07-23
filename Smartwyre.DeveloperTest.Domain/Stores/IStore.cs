namespace Smartwyre.DeveloperTest.Domain.Stores
{
    public interface IStore<TEntity>
    {
        Task<TEntity?> Get(string identifier);

        Task Update(TEntity entity);

        Task Create(TEntity entity);
    }
}
