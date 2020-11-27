using CurrencyExchange.Core.Models;
using CurrencyExchange.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.WebApi.Controllers
{
    [Route("")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        private readonly ILogger<QuoteController> _logger;
        public QuoteController(IQuoteService quoteService, ILogger<QuoteController> logger)
        {
            _quoteService = quoteService;
            _logger = logger;
        }

        [HttpGet("quote")]
        public async Task<ActionResult<string>> GetCurrentQuote(string currencyISOCode)
        {
            try
            {
                var response = await _quoteService.GetQuoteByCurrencyISOCode(currencyISOCode);
                
                return Ok(response);
            }
            catch (ArgumentNullException)
            {
                _logger.LogWarning($"GetCurrentQuote : Empty Currency Code.");
                return BadRequest();
            }
            catch (ArgumentException)
            {
                _logger.LogWarning($"GetCurrentQuote : Currency {currencyISOCode} no found.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCurrentQuote : Error looking for {currencyISOCode} Currency. Error message: {ex.Message}");
                return NoContent();
            }
        }
    }
}
