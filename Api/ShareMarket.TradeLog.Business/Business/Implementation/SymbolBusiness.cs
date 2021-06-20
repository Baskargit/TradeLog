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
    public class SymbolBusiness : ISymbolBusiness
    {
        public ISymbolRepository _symbolRepository;
        public IMapper _mapper;

        public SymbolBusiness(ISymbolRepository symbolRepository,IMapper mapper)
        {
            _symbolRepository = symbolRepository;
            _mapper = mapper;
        }

        public Biz<List<Symbol>> GetAll()
        {
            var biz = new Biz<List<Symbol>>();

            var dataEntities = _symbolRepository.Get().ToList();
            
            biz.Data = _mapper.Map<List<DE.Symbol>,List<BE.Symbol>>(dataEntities);
            return biz;
        }

        public Biz<Symbol> Get(int id)
        {
            var biz = new Biz<Symbol>();

            var dataEntity = _symbolRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new SymbolValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.Symbol());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            biz.Data = _mapper.Map<DE.Symbol,BE.Symbol>(dataEntity);
            return biz;
        }

        public Biz<Symbol> Add(Symbol entity)
        {
            var biz = new Biz<Symbol>();

            var dataEntity = _mapper.Map<BE.Symbol,DE.Symbol>(entity);

            // Validation
            var validator = new SymbolValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(entity);
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _symbolRepository.Add(dataEntity);

            biz.Data = _mapper.Map<DE.Symbol,BE.Symbol>(dataEntity);
            return biz;
        }

        public Biz<Symbol> Update(Symbol entity)
        {
            var biz = new Biz<Symbol>();

            var dataEntity = _symbolRepository.Get().FirstOrDefault(x => x.Id == entity.Id);

            // Validation
            var validator = new SymbolValidator((int)DbOperation.CREATE,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(entity);
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _mapper.Map<BE.Symbol,DE.Symbol>(entity,dataEntity);
            _symbolRepository.Update(dataEntity);

            biz.Data = _mapper.Map<DE.Symbol,BE.Symbol>(dataEntity);
            return biz;
        }

        public Biz<Symbol> Delete(int id)
        {
            var biz = new Biz<Symbol>();

            var dataEntity = _symbolRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new SymbolValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.Symbol());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }
            
            _symbolRepository.Delete(dataEntity);
            return biz;
        }

    }
}
