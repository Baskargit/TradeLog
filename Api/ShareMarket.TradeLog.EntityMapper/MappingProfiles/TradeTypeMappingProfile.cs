using System;
using AutoMapper;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.EntityMapper
{
    public class TradeTypeMappingProfile : Profile
    {
        public TradeTypeMappingProfile()
        {
            CreateMap<BE.TradeType,DE.TradeType>()
                .ForMember(dest => dest.CreatedDate , am => am.Ignore())
                .ForMember(dest => dest.ModifiedDate , am => am.Ignore());

            CreateMap<DE.TradeType,BE.TradeType>();
        }
    }
}
