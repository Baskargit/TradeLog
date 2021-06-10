using System;
using AutoMapper;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.EntityMapper
{
    public class MarketMappingProfile : Profile
    {
        public MarketMappingProfile()
        {
            CreateMap<BE.Market,DE.Market>()
                .ForMember(dest => dest.Id , am => am.Ignore())
                .ForMember(dest => dest.CreatedDate , am => am.Ignore())
                .ForMember(dest => dest.ModifiedDate , am => am.Ignore());

            CreateMap<DE.Market,BE.Market>();
        }
    }
}
