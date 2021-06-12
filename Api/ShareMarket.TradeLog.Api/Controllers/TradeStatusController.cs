using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Api.Controllers
{
    /// <summary>
    ///     TradeStatus controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TradeStatusController : ControllerBase
    {
        private ITradeStatusBusiness _tradeStatusBusiness;
        
        public TradeStatusController(ITradeStatusBusiness tradeStatusBusiness)
        {
            _tradeStatusBusiness = tradeStatusBusiness;
        }
        
        /// <summary>
        ///     Get all the available tradeStatuss
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<TradeStatus>> GetAll()
        {
            var biz = _tradeStatusBusiness.GetAll();

            if (biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Get TradeStatus by id
        /// </summary>
        /// <param name="id">TradeStatusId need to be fetched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<TradeStatus> Get(string id)
        {
            if (int.TryParse(id,out int tradeStatusId))
            {
                var biz = _tradeStatusBusiness.Get(tradeStatusId);

                if (biz.IsError) {
                    return NotFound(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Create a new TradeStatus
        /// </summary>
        /// <param name="tradeStatus">New TradeStatus information</param>
        [HttpPost]
        public ActionResult<TradeStatus> Post([FromBody]TradeStatus tradeStatus)
        {
            var biz = _tradeStatusBusiness.Add(tradeStatus);

            if(biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Update TradeStatus information
        /// </summary>
        /// <param name="tradeStatus">Updated TradeStatus information</param>
        /// <param name="id">TradeStatusId</param>
        [HttpPut("{id}")]
        public ActionResult<TradeStatus> Put([FromBody]TradeStatus tradeStatus ,string id)
        {
            if (int.TryParse(id,out int tradeStatusId))
            {
                tradeStatus.Id = tradeStatusId;
                var biz = _tradeStatusBusiness.Update(tradeStatus);

                if (biz.IsError) {
                    return BadRequest(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Delete a TradeStatus by its id
        /// </summary>
        /// <param name="id">TradeStatusid need to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<TradeStatus> Delete(string id)
        {
            if (int.TryParse(id,out int tradeStatusId))
            {
                var biz = _tradeStatusBusiness.Delete(tradeStatusId);

                if (biz.IsError) {
                    return NotFound();
                }

                return Ok();
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }
    }
}
