using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Api.Controllers
{
    /// <summary>
    ///     TradeType controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TradeTypeController : ControllerBase
    {
        private ITradeTypeBusiness _tradeTypeBusiness;
        
        public TradeTypeController(ITradeTypeBusiness tradeTypeBusiness)
        {
            _tradeTypeBusiness = tradeTypeBusiness;
        }
        
        /// <summary>
        ///     Get all the available tradeTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<TradeType>> GetAll()
        {
            var biz = _tradeTypeBusiness.GetAll();

            if (biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Get TradeType by id
        /// </summary>
        /// <param name="id">TradeTypeId need to be fetched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<TradeType> Get(string id)
        {
            if (int.TryParse(id,out int tradeTypeId))
            {
                var biz = _tradeTypeBusiness.Get(tradeTypeId);

                if (biz.IsError) {
                    return NotFound(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Create a new TradeType
        /// </summary>
        /// <param name="tradeType">New TradeType information</param>
        [HttpPost]
        public ActionResult<TradeType> Post([FromBody]TradeType tradeType)
        {
            var biz = _tradeTypeBusiness.Add(tradeType);

            if(biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Update TradeType information
        /// </summary>
        /// <param name="tradeType">Updated TradeType information</param>
        /// <param name="id">TradeTypeId</param>
        [HttpPut("{id}")]
        public ActionResult<TradeType> Put([FromBody]TradeType tradeType ,string id)
        {
            if (int.TryParse(id,out int tradeTypeId))
            {
                tradeType.Id = tradeTypeId;
                var biz = _tradeTypeBusiness.Update(tradeType);

                if (biz.IsError) {
                    return BadRequest(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Delete a TradeType by its id
        /// </summary>
        /// <param name="id">TradeTypeid need to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<TradeType> Delete(string id)
        {
            if (int.TryParse(id,out int tradeTypeId))
            {
                var biz = _tradeTypeBusiness.Delete(tradeTypeId);

                if (biz.IsError) {
                    return NotFound();
                }

                return Ok();
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }
    }
}
