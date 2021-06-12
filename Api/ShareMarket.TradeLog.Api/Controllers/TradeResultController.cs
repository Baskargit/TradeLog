using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Api.Controllers
{
    /// <summary>
    ///     TradeResult controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TradeResultController : ControllerBase
    {
        private ITradeResultBusiness _tradeResultBusiness;
        
        public TradeResultController(ITradeResultBusiness tradeResultBusiness)
        {
            _tradeResultBusiness = tradeResultBusiness;
        }
        
        /// <summary>
        ///     Get all the available tradeResults
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<TradeResult>> GetAll()
        {
            var biz = _tradeResultBusiness.GetAll();

            if (biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Get TradeResult by id
        /// </summary>
        /// <param name="id">TradeResultId need to be fetched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<TradeResult> Get(string id)
        {
            if (int.TryParse(id,out int tradeResultId))
            {
                var biz = _tradeResultBusiness.Get(tradeResultId);

                if (biz.IsError) {
                    return NotFound(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Create a new TradeResult
        /// </summary>
        /// <param name="tradeResult">New TradeResult information</param>
        [HttpPost]
        public ActionResult<TradeResult> Post([FromBody]TradeResult tradeResult)
        {
            var biz = _tradeResultBusiness.Add(tradeResult);

            if(biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Update TradeResult information
        /// </summary>
        /// <param name="tradeResult">Updated TradeResult information</param>
        /// <param name="id">TradeResultId</param>
        [HttpPut("{id}")]
        public ActionResult<TradeResult> Put([FromBody]TradeResult tradeResult ,string id)
        {
            if (int.TryParse(id,out int tradeResultId))
            {
                tradeResult.Id = tradeResultId;
                var biz = _tradeResultBusiness.Update(tradeResult);

                if (biz.IsError) {
                    return BadRequest(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Delete a TradeResult by its id
        /// </summary>
        /// <param name="id">TradeResultid need to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<TradeResult> Delete(string id)
        {
            if (int.TryParse(id,out int tradeResultId))
            {
                var biz = _tradeResultBusiness.Delete(tradeResultId);

                if (biz.IsError) {
                    return NotFound();
                }

                return Ok();
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }
    }
}
