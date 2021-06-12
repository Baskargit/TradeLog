using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;
using ShareMarket.TradeLog.BusinessEntities.Enums;
using ShareMarket.TradeLog.BusinessRules;
using ShareMarket.TradeLog.DataRepository.Interface;
using BE = ShareMarket.TradeLog.BusinessEntities;
using DE = ShareMarket.TradeLog.DataEntities;

namespace ShareMarket.TradeLog.Business.Implementation
{
    public class MarketBusiness : IMarketBusiness
    {
        public IMarketRepository _marketRepository;
        public IMapper _mapper;

        public MarketBusiness(IMarketRepository marketRepository,IMapper mapper)
        {
            _marketRepository = marketRepository;
            _mapper = mapper;
        }

        public Biz<List<Market>> GetAll()
        {
            var biz = new Biz<List<Market>>();

            var dataEntities = _marketRepository.Get().ToList();
            
            biz.Data = _mapper.Map<List<DE.Market>,List<BE.Market>>(dataEntities);
            return biz;
        }

        public Biz<Market> Get(int id)
        {
            var biz = new Biz<Market>();

            var dataEntity = _marketRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new MarketValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.Market());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            biz.Data = _mapper.Map<DE.Market,BE.Market>(dataEntity);
            return biz;
        }

        public Biz<Market> Add(Market entity)
        {
            var biz = new Biz<Market>();

            var dataEntity = _mapper.Map<BE.Market,DE.Market>(entity);

            // Validation
            var validator = new MarketValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(new BE.Market());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _marketRepository.Add(dataEntity);

            biz.Data = _mapper.Map<DE.Market,BE.Market>(dataEntity);
            return biz;
        }

        public Biz<Market> Update(Market entity)
        {
            var biz = new Biz<Market>();

            var dataEntity = _mapper.Map<BE.Market,DE.Market>(entity);

            // Validation
            var validator = new MarketValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(new BE.Market());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _marketRepository.Update(dataEntity);

            biz.Data = _mapper.Map<DE.Market,BE.Market>(dataEntity);
            return biz;
        }

        public Biz<Market> Delete(int id)
        {
            var biz = new Biz<Market>();

            var dataEntity = _marketRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new MarketValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.Market());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }
            
            _marketRepository.Delete(dataEntity);
            return biz;
        }

    }
}
