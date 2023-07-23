using AutoMapper;
using Smartwyre.DeveloperTest.Data.DbModels;
using Smartwyre.DeveloperTest.Domain.Entities;
using Smartwyre.DeveloperTest.Domain.Enums;

namespace Smartwyre.DeveloperTest.Data.Mappings;

public class RebateProfile : Profile
{
    public RebateProfile()
    {
        CreateMap<Rebate, RebateDb>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<RebateCalculation, RebateCalculationDb>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IncentiveType, opt => opt.MapFrom<IncentiveTypeResolver>());
    }
}

public class IncentiveTypeResolver : IValueResolver<RebateCalculation, RebateCalculationDb, IncentiveType>
{
    public IncentiveType Resolve(RebateCalculation source, RebateCalculationDb destination, IncentiveType destMember, ResolutionContext context)
    {
        return source switch
        {
            FixedAmountRebateCalculation => IncentiveType.FixedCashAmount,
            FixedRateRebateCalculation => IncentiveType.FixedRateRebate,
            AmountPerUomRebateCalculation => IncentiveType.AmountPerUom,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
