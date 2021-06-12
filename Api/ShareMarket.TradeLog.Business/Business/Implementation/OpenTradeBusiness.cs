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
    public class OpenTradeBusiness : IOpenTradeBusiness
    {
        public IOpenTradeRepository _openTradeRepository;
        public IMapper _mapper;

        public OpenTradeBusiness(IOpenTradeRepository openTradeRepository,IMapper mapper)
        {
            _openTradeRepository = openTradeRepository;
            _mapper = mapper;
        }

        public Biz<List<OpenTrade>> GetAll()
        {
            var biz = new Biz<List<OpenTrade>>();

            var dataEntities = _openTradeRepository.Get().ToList();
            
            biz.Data = _mapper.Map<List<DE.OpenTrade>,List<BE.OpenTrade>>(dataEntities);
            return biz;
        }

        public Biz<OpenTrade> Get(int id)
        {
            var biz = new Biz<OpenTrade>();

            var dataEntity = _openTradeRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new OpenTradeValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.OpenTrade());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            biz.Data = _mapper.Map<DE.OpenTrade,BE.OpenTrade>(dataEntity);
            return biz;
        }

        public Biz<OpenTrade> Add(OpenTrade entity)
        {
            var biz = new Biz<OpenTrade>();

            var dataEntity = _mapper.Map<BE.OpenTrade,DE.OpenTrade>(entity);

            // Validation
            var validator = new OpenTradeValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(new BE.OpenTrade());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _openTradeRepository.Add(dataEntity);

            biz.Data = _mapper.Map<DE.OpenTrade,BE.OpenTrade>(dataEntity);
            return biz;
        }

        public Biz<OpenTrade> Update(OpenTrade entity)
        {
            var biz = new Biz<OpenTrade>();

            var dataEntity = _openTradeRepository.Get().FirstOrDefault(x => x.Id == entity.Id);

            // Validation
            var validator = new OpenTradeValidator((int)DbOperation.CREATE,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.OpenTrade());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _mapper.Map<BE.OpenTrade,DE.OpenTrade>(entity,dataEntity);
            _openTradeRepository.Update(dataEntity);

            biz.Data = _mapper.Map<DE.OpenTrade,BE.OpenTrade>(dataEntity);
            return biz;
        }

        public Biz<OpenTrade> Delete(int id)
        {
            var biz = new Biz<OpenTrade>();

            var dataEntity = _openTradeRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new OpenTradeValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.OpenTrade());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }
            
            _openTradeRepository.Delete(dataEntity);
            return biz;
        }

    }
}
