using AutoMapper;
using Smartwyre.DeveloperTest.Data.Db;
using Smartwyre.DeveloperTest.Data.DbModels;
using Smartwyre.DeveloperTest.Domain.Entities;

namespace Smartwyre.DeveloperTest.Data.Stores;

public class ProductDataStore : StoreBase<Product, ProductDb>
{
    public ProductDataStore(IDbContext context, IMapper mapper) : base(context.Products, mapper)
    {
    }
}
