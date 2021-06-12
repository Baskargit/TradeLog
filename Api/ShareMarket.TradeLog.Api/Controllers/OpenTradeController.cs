using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Api.Controllers
{
    /// <summary>
    ///     OpenTrade controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OpenTradeController : ControllerBase
    {
        private IOpenTradeBusiness _openTradeBusiness;
        
        public OpenTradeController(IOpenTradeBusiness openTradeBusiness)
        {
            _openTradeBusiness = openTradeBusiness;
        }
        
        /// <summary>
        ///     Get all the available openTrades
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<OpenTrade>> GetAll()
        {
            var biz = _openTradeBusiness.GetAll();

            if (biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Get OpenTrade by id
        /// </summary>
        /// <param name="id">OpenTradeId need to be fetched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<OpenTrade> Get(string id)
        {
            if (int.TryParse(id,out int openTradeId))
            {
                var biz = _openTradeBusiness.Get(openTradeId);

                if (biz.IsError) {
                    return NotFound(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Create a new OpenTrade
        /// </summary>
        /// <param name="openTrade">New OpenTrade information</param>
        [HttpPost]
        public ActionResult<OpenTrade> Post([FromBody]OpenTrade openTrade)
        {
            var biz = _openTradeBusiness.Add(openTrade);

            if(biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Update OpenTrade information
        /// </summary>
        /// <param name="openTrade">Updated OpenTrade information</param>
        /// <param name="id">OpenTradeId</param>
        [HttpPut("{id}")]
        public ActionResult<OpenTrade> Put([FromBody]OpenTrade openTrade ,string id)
        {
            if (int.TryParse(id,out int openTradeId))
            {
                openTrade.Id = openTradeId;
                var biz = _openTradeBusiness.Update(openTrade);

                if (biz.IsError) {
                    return BadRequest(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Delete a OpenTrade by its id
        /// </summary>
        /// <param name="id">OpenTradeid need to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<OpenTrade> Delete(string id)
        {
            if (int.TryParse(id,out int openTradeId))
            {
                var biz = _openTradeBusiness.Delete(openTradeId);

                if (biz.IsError) {
                    return NotFound();
                }

                return Ok();
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }
    }
}
