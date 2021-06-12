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
    public class TradeTypeBusiness : ITradeTypeBusiness
    {
        public ITradeTypeRepository _tradeTypeRepository;
        public IMapper _mapper;

        public TradeTypeBusiness(ITradeTypeRepository tradeTypeRepository,IMapper mapper)
        {
            _tradeTypeRepository = tradeTypeRepository;
            _mapper = mapper;
        }

        public Biz<List<TradeType>> GetAll()
        {
            var biz = new Biz<List<TradeType>>();

            var dataEntities = _tradeTypeRepository.Get().ToList();
            
            biz.Data = _mapper.Map<List<DE.TradeType>,List<BE.TradeType>>(dataEntities);
            return biz;
        }

        public Biz<TradeType> Get(int id)
        {
            var biz = new Biz<TradeType>();

            var dataEntity = _tradeTypeRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new TradeTypeValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeType());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            biz.Data = _mapper.Map<DE.TradeType,BE.TradeType>(dataEntity);
            return biz;
        }

        public Biz<TradeType> Add(TradeType entity)
        {
            var biz = new Biz<TradeType>();

            var dataEntity = _mapper.Map<BE.TradeType,DE.TradeType>(entity);

            // Validation
            var validator = new TradeTypeValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(new BE.TradeType());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _tradeTypeRepository.Add(dataEntity);

            biz.Data = _mapper.Map<DE.TradeType,BE.TradeType>(dataEntity);
            return biz;
        }

        public Biz<TradeType> Update(TradeType entity)
        {
            var biz = new Biz<TradeType>();

            var dataEntity = _tradeTypeRepository.Get().FirstOrDefault(x => x.Id == entity.Id);

            // Validation
            var validator = new TradeTypeValidator((int)DbOperation.CREATE,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeType());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _mapper.Map<BE.TradeType,DE.TradeType>(entity,dataEntity);
            _tradeTypeRepository.Update(dataEntity);

            biz.Data = _mapper.Map<DE.TradeType,BE.TradeType>(dataEntity);
            return biz;
        }

        public Biz<TradeType> Delete(int id)
        {
            var biz = new Biz<TradeType>();

            var dataEntity = _tradeTypeRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new TradeTypeValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeType());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }
            
            _tradeTypeRepository.Delete(dataEntity);
            return biz;
        }

    }
}
