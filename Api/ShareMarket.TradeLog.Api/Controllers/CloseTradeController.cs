using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Api.Controllers
{
    /// <summary>
    ///     CloseTrade controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CloseTradeController : ControllerBase
    {
        private ICloseTradeBusiness _closeTradeBusiness;
        
        public CloseTradeController(ICloseTradeBusiness closeTradeBusiness)
        {
            _closeTradeBusiness = closeTradeBusiness;
        }
        
        /// <summary>
        ///     Get all the available closeTrades
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<CloseTrade>> GetAll()
        {
            var biz = _closeTradeBusiness.GetAll();

            if (biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Get CloseTrade by id
        /// </summary>
        /// <param name="id">CloseTradeId need to be fetched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<CloseTrade> Get(string id)
        {
            if (int.TryParse(id,out int closeTradeId))
            {
                var biz = _closeTradeBusiness.Get(closeTradeId);

                if (biz.IsError) {
                    return NotFound(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Create a new CloseTrade
        /// </summary>
        /// <param name="closeTrade">New CloseTrade information</param>
        [HttpPost]
        public ActionResult<CloseTrade> Post([FromBody]CloseTrade closeTrade)
        {
            var biz = _closeTradeBusiness.Add(closeTrade);

            if(biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Update CloseTrade information
        /// </summary>
        /// <param name="closeTrade">Updated CloseTrade information</param>
        /// <param name="id">CloseTradeId</param>
        [HttpPut("{id}")]
        public ActionResult<CloseTrade> Put([FromBody]CloseTrade closeTrade ,string id)
        {
            if (int.TryParse(id,out int closeTradeId))
            {
                closeTrade.Id = closeTradeId;
                var biz = _closeTradeBusiness.Update(closeTrade);

                if (biz.IsError) {
                    return BadRequest(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Delete a CloseTrade by its id
        /// </summary>
        /// <param name="id">CloseTradeid need to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<CloseTrade> Delete(string id)
        {
            if (int.TryParse(id,out int closeTradeId))
            {
                var biz = _closeTradeBusiness.Delete(closeTradeId);

                if (biz.IsError) {
                    return NotFound();
                }

                return Ok();
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }
    }
}
