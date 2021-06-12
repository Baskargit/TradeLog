using System;
using AutoMapper;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.EntityMapper
{
    public class TradeResultMappingProfile : Profile
    {
        public TradeResultMappingProfile()
        {
            CreateMap<BE.TradeResult,DE.TradeResult>()
                .ForMember(dest => dest.CreatedDate , am => am.Ignore())
                .ForMember(dest => dest.ModifiedDate , am => am.Ignore());

            CreateMap<DE.TradeResult,BE.TradeResult>();
        }
    }
}
