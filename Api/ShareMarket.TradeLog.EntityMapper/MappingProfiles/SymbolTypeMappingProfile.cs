using System;
using AutoMapper;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.EntityMapper
{
    public class SymbolTypeMappingProfile : Profile
    {
        public SymbolTypeMappingProfile()
        {
            CreateMap<BE.SymbolType,DE.SymbolType>()
                .ForMember(dest => dest.CreatedDate , am => am.Ignore())
                .ForMember(dest => dest.ModifiedDate , am => am.Ignore());

            CreateMap<DE.SymbolType,BE.SymbolType>();
        }
    }
}
