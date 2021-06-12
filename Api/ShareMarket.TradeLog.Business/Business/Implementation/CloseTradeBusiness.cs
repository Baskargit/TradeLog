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
    public class CloseTradeBusiness : ICloseTradeBusiness
    {
        public ICloseTradeRepository _closeTradeRepository;
        public IMapper _mapper;

        public CloseTradeBusiness(ICloseTradeRepository closeTradeRepository,IMapper mapper)
        {
            _closeTradeRepository = closeTradeRepository;
            _mapper = mapper;
        }

        public Biz<List<CloseTrade>> GetAll()
        {
            var biz = new Biz<List<CloseTrade>>();

            var dataEntities = _closeTradeRepository.Get().ToList();
            
            biz.Data = _mapper.Map<List<DE.CloseTrade>,List<BE.CloseTrade>>(dataEntities);
            return biz;
        }

        public Biz<CloseTrade> Get(int id)
        {
            var biz = new Biz<CloseTrade>();

            var dataEntity = _closeTradeRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new CloseTradeValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.CloseTrade());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            biz.Data = _mapper.Map<DE.CloseTrade,BE.CloseTrade>(dataEntity);
            return biz;
        }

        public Biz<CloseTrade> Add(CloseTrade entity)
        {
            var biz = new Biz<CloseTrade>();

            var dataEntity = _mapper.Map<BE.CloseTrade,DE.CloseTrade>(entity);

            // Validation
            var validator = new CloseTradeValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(new BE.CloseTrade());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _closeTradeRepository.Add(dataEntity);

            biz.Data = _mapper.Map<DE.CloseTrade,BE.CloseTrade>(dataEntity);
            return biz;
        }

        public Biz<CloseTrade> Update(CloseTrade entity)
        {
            var biz = new Biz<CloseTrade>();

            var dataEntity = _closeTradeRepository.Get().FirstOrDefault(x => x.Id == entity.Id);

            // Validation
            var validator = new CloseTradeValidator((int)DbOperation.CREATE,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.CloseTrade());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _mapper.Map<BE.CloseTrade,DE.CloseTrade>(entity,dataEntity);
            _closeTradeRepository.Update(dataEntity);

            biz.Data = _mapper.Map<DE.CloseTrade,BE.CloseTrade>(dataEntity);
            return biz;
        }

        public Biz<CloseTrade> Delete(int id)
        {
            var biz = new Biz<CloseTrade>();

            var dataEntity = _closeTradeRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new CloseTradeValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.CloseTrade());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }
            
            _closeTradeRepository.Delete(dataEntity);
            return biz;
        }

    }
}
