using CurrencyExchange.Core.ModelsDTO;
using CurrencyExchange.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.WebApi.Controllers
{
    [Route("")]
    [ApiController]
    public class PurchaseController:ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost("purchase")]
        public async Task<ActionResult> Post(PurchaseDTO purchase)
        {

            var reslult = await _purchaseService.CreatePurchase(purchase);

            if (reslult != null)
            {
                return Ok(purchase);
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
