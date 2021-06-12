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
    public class TradeStatusBusiness : ITradeStatusBusiness
    {
        public ITradeStatusRepository _tradeStatusRepository;
        public IMapper _mapper;

        public TradeStatusBusiness(ITradeStatusRepository tradeStatusRepository,IMapper mapper)
        {
            _tradeStatusRepository = tradeStatusRepository;
            _mapper = mapper;
        }

        public Biz<List<TradeStatus>> GetAll()
        {
            var biz = new Biz<List<TradeStatus>>();

            var dataEntities = _tradeStatusRepository.Get().ToList();
            
            biz.Data = _mapper.Map<List<DE.TradeStatus>,List<BE.TradeStatus>>(dataEntities);
            return biz;
        }

        public Biz<TradeStatus> Get(int id)
        {
            var biz = new Biz<TradeStatus>();

            var dataEntity = _tradeStatusRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new TradeStatusValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeStatus());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            biz.Data = _mapper.Map<DE.TradeStatus,BE.TradeStatus>(dataEntity);
            return biz;
        }

        public Biz<TradeStatus> Add(TradeStatus entity)
        {
            var biz = new Biz<TradeStatus>();

            var dataEntity = _mapper.Map<BE.TradeStatus,DE.TradeStatus>(entity);

            // Validation
            var validator = new TradeStatusValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(new BE.TradeStatus());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _tradeStatusRepository.Add(dataEntity);

            biz.Data = _mapper.Map<DE.TradeStatus,BE.TradeStatus>(dataEntity);
            return biz;
        }

        public Biz<TradeStatus> Update(TradeStatus entity)
        {
            var biz = new Biz<TradeStatus>();

            var dataEntity = _tradeStatusRepository.Get().FirstOrDefault(x => x.Id == entity.Id);

            // Validation
            var validator = new TradeStatusValidator((int)DbOperation.CREATE,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeStatus());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _mapper.Map<BE.TradeStatus,DE.TradeStatus>(entity,dataEntity);
            _tradeStatusRepository.Update(dataEntity);

            biz.Data = _mapper.Map<DE.TradeStatus,BE.TradeStatus>(dataEntity);
            return biz;
        }

        public Biz<TradeStatus> Delete(int id)
        {
            var biz = new Biz<TradeStatus>();

            var dataEntity = _tradeStatusRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new TradeStatusValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.TradeStatus());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }
            
            _tradeStatusRepository.Delete(dataEntity);
            return biz;
        }

    }
}
