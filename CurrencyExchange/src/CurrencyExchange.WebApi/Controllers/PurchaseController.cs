using CurrencyExchange.Core.ModelsDTO;
using CurrencyExchange.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.WebApi.Controllers
{
    [Route("")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        private readonly ILogger<PurchaseController> _logger;
        public PurchaseController(IPurchaseService purchaseService, ILogger<PurchaseController> logger)
        {
            _purchaseService = purchaseService;
            _logger = logger;
        }

        [HttpPost("purchase")]
        public async Task<ActionResult> Post(PurchaseDTO purchase)
        {
            try
            {
                var reslult = await _purchaseService.CreatePurchase(purchase);

                if (reslult != null)
                {
                    _logger.LogInformation($"Post : Success. User: {purchase.UserId}. Currency {purchase.ISOCurrencyCode}. Amount: {purchase.AmountARGCurrency}");
                    return Ok(purchase);
                }
                else
                {
                    _logger.LogInformation($"Post : Purchase limit exceeded. User: {purchase.UserId}. Currency {purchase.ISOCurrencyCode}. Amount: {purchase.AmountARGCurrency}.");
                    return Unauthorized();
                }
            }
            catch(ArgumentException)
            {
                _logger.LogInformation($"Post : Error for Currency {purchase.ISOCurrencyCode}. User: {purchase.UserId}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Post : Error creating Purchase. User: {purchase.UserId}, Currency: {purchase.ISOCurrencyCode}, Amount: {purchase.AmountARGCurrency}. Error message: {ex.Message}");
                return NoContent();
            }


        }
    }
}
