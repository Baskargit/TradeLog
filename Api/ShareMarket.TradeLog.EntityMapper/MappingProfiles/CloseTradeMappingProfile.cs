using System;
using AutoMapper;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.EntityMapper
{
    public class CloseTradeMappingProfile : Profile
    {
        public CloseTradeMappingProfile()
        {
            CreateMap<BE.CloseTrade,DE.CloseTrade>()
                .ForMember(dest => dest.CreatedDate , am => am.Ignore())
                .ForMember(dest => dest.ModifiedDate , am => am.Ignore());

            CreateMap<DE.CloseTrade,BE.CloseTrade>();
        }
    }
}
