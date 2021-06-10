using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.BusinessEntities;

namespace ShareMarket.TradeLog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarketController : ControllerBase
    {
        public IMarketBusiness _marketBusiness;
        
        public MarketController(IMarketBusiness marketBusiness)
        {
            this._marketBusiness = marketBusiness;
        }
        
        /// <summary>
        ///     Get all the available markets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Market>> GetAll()
        {
            var biz = _marketBusiness.GetAll();

            if (biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Get Market by id
        /// </summary>
        /// <param name="id">MarketId need to be fetched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Market> Get(string id)
        {
            if (int.TryParse(id,out int marketId))
            {
                var biz = _marketBusiness.Get(marketId);

                if (biz.IsError) {
                    return NotFound(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Create a new Market
        /// </summary>
        /// <param name="market">New Market information</param>
        [HttpPost]
        public ActionResult<Market> Post([FromBody]Market market)
        {
            var biz = _marketBusiness.Add(market);

            if(biz.IsError) {
                return BadRequest(biz.Errors);
            }

            return Ok(biz.Data);
        }

        /// <summary>
        ///     Update Market information
        /// </summary>
        /// <param name="market">Updated Market information</param>
        /// <param name="id">MarketId</param>
        [HttpPut("{id}")]
        public ActionResult<Market> Put([FromBody]Market market ,string id)
        {
            if (int.TryParse(id,out int marketId))
            {
                market.Id = marketId;
                var biz = _marketBusiness.Update(market);

                if (biz.IsError) {
                    return BadRequest(biz.Errors);
                }

                return Ok(biz.Data);
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }

        /// <summary>
        ///     Delete a Market by its id
        /// </summary>
        /// <param name="id">Marketid need to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<Market> Delete(string id)
        {
            if (int.TryParse(id,out int marketId))
            {
                var biz = _marketBusiness.Delete(marketId);

                if (biz.IsError) {
                    return NotFound();
                }

                return Ok();
            }
            return BadRequest(Error.GetError("1001","Invalid id provided"));
        }
    }
}
