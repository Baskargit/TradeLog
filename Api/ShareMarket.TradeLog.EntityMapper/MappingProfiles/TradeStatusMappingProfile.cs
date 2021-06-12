using System;
using AutoMapper;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.EntityMapper
{
    public class TradeStatusMappingProfile : Profile
    {
        public TradeStatusMappingProfile()
        {
            CreateMap<BE.TradeStatus,DE.TradeStatus>()
                .ForMember(dest => dest.CreatedDate , am => am.Ignore())
                .ForMember(dest => dest.ModifiedDate , am => am.Ignore());

            CreateMap<DE.TradeStatus,BE.TradeStatus>();
        }
    }
}
