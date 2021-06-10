using System;
using AutoMapper;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.EntityMapper
{
    public class SymbolMappingProfile : Profile
    {
        public SymbolMappingProfile()
        {
            CreateMap<BE.Symbol,DE.Symbol>()
                .ForMember(dest => dest.CreatedDate , am => am.Ignore())
                .ForMember(dest => dest.ModifiedDate , am => am.Ignore());

            CreateMap<DE.Symbol,BE.Symbol>();
        }
    }
}
