using AutoMapper;
using Smartwyre.DeveloperTest.Data.Db;
using Smartwyre.DeveloperTest.Data.DbModels;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Factories;

namespace Smartwyre.DeveloperTest.Data.Stores;

public class RebateCalculationStore : StoreBase<RebateCalculation, RebateCalculationDb>
{
    public RebateCalculationStore(IDbContext context, IMapper mapper) : base(context.RebateCalculations, mapper)
    {
    }

    public override Task<RebateCalculation?> Get(string identifier)
    {
        var dbModel = _collection.FirstOrDefault(x => x.Identifier == identifier);

        var result = dbModel != null ? BuildEntityInstance(dbModel) : null;

        return Task.FromResult(result);
    }

    private RebateCalculation BuildEntityInstance(RebateCalculationDb dbModel)
    {
        var entity = RebateCalculationFactory.GetInstance(dbModel.IncentiveType);

        _mapper.Map(dbModel, entity);

        return entity;
    }
}
