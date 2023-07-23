using AutoMapper;
using Smartwyre.DeveloperTest.Data.DbModels;
using Smartwyre.DeveloperTest.Domain.Entities;

namespace Smartwyre.DeveloperTest.Data.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDb>().ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ProductDb, Product>();
    }
}
