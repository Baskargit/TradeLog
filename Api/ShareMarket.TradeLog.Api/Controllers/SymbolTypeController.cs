using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Api.Controllers
{
    /// <summary>
    ///     SymbolType controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SymbolTypeController : ControllerBase
    {
        private ISymbolTypeBusiness _symbolTypeBusiness;
        
        public SymbolTypeController(ISymbolTypeBusiness symbolTypeBusiness)
        {
            _symbolTypeBusiness = symbolTypeBusiness;
        }
        
        /// <summary>
        ///     Get all the available symbolTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<SymbolType>> GetAll()
        {
            var biz = _symbolTypeBusiness.GetAll();

            if (biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Get SymbolType by id
        /// </summary>
        /// <param name="id">SymbolTypeId need to be fetched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<SymbolType> Get(string id)
        {
            if (int.TryParse(id,out int symbolTypeId))
            {
                var biz = _symbolTypeBusiness.Get(symbolTypeId);

                if (biz.IsError) {
                    return NotFound(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Create a new SymbolType
        /// </summary>
        /// <param name="symbolType">New SymbolType information</param>
        [HttpPost]
        public ActionResult<SymbolType> Post([FromBody]SymbolType symbolType)
        {
            var biz = _symbolTypeBusiness.Add(symbolType);

            if(biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Update SymbolType information
        /// </summary>
        /// <param name="symbolType">Updated SymbolType information</param>
        /// <param name="id">SymbolTypeId</param>
        [HttpPut("{id}")]
        public ActionResult<SymbolType> Put([FromBody]SymbolType symbolType ,string id)
        {
            if (int.TryParse(id,out int symbolTypeId))
            {
                symbolType.Id = symbolTypeId;
                var biz = _symbolTypeBusiness.Update(symbolType);

                if (biz.IsError) {
                    return BadRequest(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Delete a SymbolType by its id
        /// </summary>
        /// <param name="id">SymbolTypeid need to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<SymbolType> Delete(string id)
        {
            if (int.TryParse(id,out int symbolTypeId))
            {
                var biz = _symbolTypeBusiness.Delete(symbolTypeId);

                if (biz.IsError) {
                    return NotFound();
                }

                return Ok();
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }
    }
}
