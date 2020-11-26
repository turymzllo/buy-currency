using CurrencyExchange.Core.Models;
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
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet("quote")]
        public async Task<ActionResult<string>> GetCurrentQuote(string currecyISOCode)
        {
            try
            {
                var response = await _quoteService.GetQuoteByCurrencyISOCode(currecyISOCode);
                return Ok(response);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return NoContent();
            }
        }
    }
}
