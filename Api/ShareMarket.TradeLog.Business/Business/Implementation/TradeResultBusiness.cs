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
    public class TradeResultBusiness : ITradeResultBusiness
    {
        public ITradeResultRepository _tradeResultRepository;
        public IMapper _mapper;

        public TradeResultBusiness(ITradeResultRepository tradeResultRepository,IMapper mapper)
        {
            _tradeResultRepository = tradeResultRepository;
            _mapper = mapper;
        }

        public Biz<List<TradeResult>> GetAll()
        {
            var biz = new Biz<List<TradeResult>>();

            var dataEntities = _tradeResultRepository.Get().ToList();
            
            biz.Data = _mapper.Map<List<DE.TradeResult>,List<BE.TradeResult>>(dataEntities);
            return biz;
        }

        public Biz<TradeResult> Get(int id)
        {
            var biz = new Biz<TradeResult>();

            var dataEntity = _tradeResultRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new TradeResultValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeResult());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            biz.Data = _mapper.Map<DE.TradeResult,BE.TradeResult>(dataEntity);
            return biz;
        }

        public Biz<TradeResult> Add(TradeResult entity)
        {
            var biz = new Biz<TradeResult>();

            var dataEntity = _mapper.Map<BE.TradeResult,DE.TradeResult>(entity);

            // Validation
            var validator = new TradeResultValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(new BE.TradeResult());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _tradeResultRepository.Add(dataEntity);

            biz.Data = _mapper.Map<DE.TradeResult,BE.TradeResult>(dataEntity);
            return biz;
        }

        public Biz<TradeResult> Update(TradeResult entity)
        {
            var biz = new Biz<TradeResult>();

            var dataEntity = _tradeResultRepository.Get().FirstOrDefault(x => x.Id == entity.Id);

            // Validation
            var validator = new TradeResultValidator((int)DbOperation.CREATE,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeResult());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _mapper.Map<BE.TradeResult,DE.TradeResult>(entity,dataEntity);
            _tradeResultRepository.Update(dataEntity);

            biz.Data = _mapper.Map<DE.TradeResult,BE.TradeResult>(dataEntity);
            return biz;
        }

        public Biz<TradeResult> Delete(int id)
        {
            var biz = new Biz<TradeResult>();

            var dataEntity = _tradeResultRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new TradeResultValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeResult());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }
            
            _tradeResultRepository.Delete(dataEntity);
            return biz;
        }

    }
}
