using AutoMapper;
using Smartwyre.DeveloperTest.Data.Db;
using Smartwyre.DeveloperTest.Data.DbModels;
using Smartwyre.DeveloperTest.Domain.Entities;

namespace Smartwyre.DeveloperTest.Data.Stores;

public class RebateDataStore: StoreBase<Rebate, RebateDb>
{
    public RebateDataStore(IDbContext context, IMapper mapper) : base(context.Rebates, mapper)
    {
    }
}
