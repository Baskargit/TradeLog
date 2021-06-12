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
    public class SymbolTypeBusiness : ISymbolTypeBusiness
    {
        public ISymbolTypeRepository _symbolTypeTypeRepository;
        public IMapper _mapper;

        public SymbolTypeBusiness(ISymbolTypeRepository symbolTypeTypeRepository,IMapper mapper)
        {
            _symbolTypeTypeRepository = symbolTypeTypeRepository;
            _mapper = mapper;
        }

        public Biz<List<SymbolType>> GetAll()
        {
            var biz = new Biz<List<SymbolType>>();

            var dataEntities = _symbolTypeTypeRepository.Get().ToList();
            
            biz.Data = _mapper.Map<List<DE.SymbolType>,List<BE.SymbolType>>(dataEntities);
            return biz;
        }

        public Biz<SymbolType> Get(int id)
        {
            var biz = new Biz<SymbolType>();

            var dataEntity = _symbolTypeTypeRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new SymbolTypeValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.SymbolType());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            biz.Data = _mapper.Map<DE.SymbolType,BE.SymbolType>(dataEntity);
            return biz;
        }

        public Biz<SymbolType> Add(SymbolType entity)
        {
            var biz = new Biz<SymbolType>();

            var dataEntity = _mapper.Map<BE.SymbolType,DE.SymbolType>(entity);

            // Validation
            var validator = new SymbolTypeValidator((int)DbOperation.CREATE);
            var validationResult = validator.Validate(new BE.SymbolType());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _symbolTypeTypeRepository.Add(dataEntity);

            biz.Data = _mapper.Map<DE.SymbolType,BE.SymbolType>(dataEntity);
            return biz;
        }

        public Biz<SymbolType> Update(SymbolType entity)
        {
            var biz = new Biz<SymbolType>();

            var dataEntity = _symbolTypeTypeRepository.Get().FirstOrDefault(x => x.Id == entity.Id);

            // Validation
            var validator = new SymbolTypeValidator((int)DbOperation.CREATE,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.SymbolType());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }

            _mapper.Map<BE.SymbolType,DE.SymbolType>(entity,dataEntity);
            _symbolTypeTypeRepository.Update(dataEntity);

            biz.Data = _mapper.Map<DE.SymbolType,BE.SymbolType>(dataEntity);
            return biz;
        }

        public Biz<SymbolType> Delete(int id)
        {
            var biz = new Biz<SymbolType>();

            var dataEntity = _symbolTypeTypeRepository.GetWith(x => x.Id == id).FirstOrDefault();

            // Validation
            var validator = new SymbolTypeValidator((int)DbOperation.READ,(dataEntity != null) ? true : false);
            var validationResult = validator.Validate(new BE.SymbolType());
            if(!validationResult.IsValid) {
                validationResult.Errors.ToList().ForEach(error => 
                    biz.AddError(error.ErrorCode,error.ErrorMessage)
                );
                return biz;
            }
            
            _symbolTypeTypeRepository.Delete(dataEntity);
            return biz;
        }

    }
}
