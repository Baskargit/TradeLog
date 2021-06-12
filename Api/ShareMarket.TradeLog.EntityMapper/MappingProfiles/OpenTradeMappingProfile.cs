using System;
using AutoMapper;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.EntityMapper
{
    public class OpenTradeMappingProfile : Profile
    {
        public OpenTradeMappingProfile()
        {
            CreateMap<BE.OpenTrade,DE.OpenTrade>()
                .ForMember(dest => dest.CreatedDate , am => am.Ignore())
                .ForMember(dest => dest.ModifiedDate , am => am.Ignore());

            CreateMap<DE.OpenTrade,BE.OpenTrade>();
        }
    }
}
