using AutoMapper;
using Smartwyre.DeveloperTest.Data.DbModels;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Stores;

namespace Smartwyre.DeveloperTest.Data.Stores;

public abstract class StoreBase<TEntity, TDbModel> : IStore<TEntity>
    where TEntity : class, IDomainEntity
    where TDbModel : IDbModel
{
    protected readonly IList<TDbModel> _collection;
    protected readonly IMapper _mapper;

    internal StoreBase(IList<TDbModel> collection, IMapper mapper)
    {
        _collection = collection;
        _mapper = mapper;
    }

    public virtual Task<TEntity?> Get(string identifier)
    {
        var dbModel = _collection.FirstOrDefault(x => x.Identifier == identifier);

        var result = dbModel != null ? _mapper.Map<TEntity>(dbModel) : null;

        return Task.FromResult(result);
    }

    public virtual Task Update(TEntity entity)
    {
        var dbModel = _collection.FirstOrDefault(x => x.Identifier == entity.Identifier);

        if (dbModel == null)
        {
            throw new KeyNotFoundException($"No object found to update for identifier {entity.Identifier}.");
        }

        _mapper.Map(entity, dbModel);

        return Task.CompletedTask;
    }

    public virtual Task Create(TEntity entity)
    {
        var dbModel = _mapper.Map<TDbModel>(entity);

        // Would be done by db
        dbModel.Id = Guid.NewGuid().ToString();

        _collection.Add(dbModel);

        return Task.CompletedTask;
    }
}
