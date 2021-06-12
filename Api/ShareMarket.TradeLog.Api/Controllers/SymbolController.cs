using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Api.Controllers
{
    /// <summary>
    ///     Symbol controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SymbolController : ControllerBase
    {
        private ISymbolBusiness _symbolBusiness;
        
        public SymbolController(ISymbolBusiness symbolBusiness)
        {
            _symbolBusiness = symbolBusiness;
        }
        
        /// <summary>
        ///     Get all the available symbols
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Symbol>> GetAll()
        {
            var biz = _symbolBusiness.GetAll();

            if (biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Get Symbol by id
        /// </summary>
        /// <param name="id">SymbolId need to be fetched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Symbol> Get(string id)
        {
            if (int.TryParse(id,out int symbolId))
            {
                var biz = _symbolBusiness.Get(symbolId);

                if (biz.IsError) {
                    return NotFound(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Create a new Symbol
        /// </summary>
        /// <param name="symbol">New Symbol information</param>
        [HttpPost]
        public ActionResult<Symbol> Post([FromBody]Symbol symbol)
        {
            var biz = _symbolBusiness.Add(symbol);

            if(biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Update Symbol information
        /// </summary>
        /// <param name="symbol">Updated Symbol information</param>
        /// <param name="id">SymbolId</param>
        [HttpPut("{id}")]
        public ActionResult<Symbol> Put([FromBody]Symbol symbol ,string id)
        {
            if (int.TryParse(id,out int symbolId))
            {
                symbol.Id = symbolId;
                var biz = _symbolBusiness.Update(symbol);

                if (biz.IsError) {
                    return BadRequest(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Delete a Symbol by its id
        /// </summary>
        /// <param name="id">Symbolid need to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<Symbol> Delete(string id)
        {
            if (int.TryParse(id,out int symbolId))
            {
                var biz = _symbolBusiness.Delete(symbolId);

                if (biz.IsError) {
                    return NotFound();
                }

                return Ok();
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }
    }
}
