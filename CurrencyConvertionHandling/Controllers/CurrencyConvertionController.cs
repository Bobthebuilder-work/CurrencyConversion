using Conversion.Domain.Models.DTO.ConversionIO;
using Conversion.Domain.Models.DTO.HistoricRatesIO;
using Conversion.Domain.Models.DTO.LatestExchangeIO;
using Conversion_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Data;

namespace CurrencyConvertionHandling.Controllers
{
    [Route("api/v1/[controller]")]
    [EnableRateLimiting("JwtPolicy")]
    [ApiController]
    public class CurrencyConvertionController : ControllerBase
    {
        private readonly ICurrencyConvertionService _currencyConvertionService;

        public CurrencyConvertionController(ICurrencyConvertionService currencyConvertionService)
        {
            _currencyConvertionService = currencyConvertionService;
        }

        [Authorize(Roles ="admin,user")]
        [HttpGet("ExchangeRates")]
        public async Task<IActionResult> LatestExchangeRates([FromQuery] LatestExchangeRatesRequest latestExchangeRates)
        {
            return Ok(await _currencyConvertionService.GetExchangeRatesAsync(latestExchangeRates));
        }

        [Authorize(Roles ="adminuser")]
        [HttpGet("ConvertionRate")]
        public async Task<IActionResult> ConversionRate([FromQuery] ConvertRequest convertRequest)
        {
            return Ok(await _currencyConvertionService.ConvertAsync(convertRequest));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("HistoricRates")]
        public async Task<IActionResult> HistoricRates([FromQuery] HistoricRatesRequest historicRates)
        {
            return Ok(await _currencyConvertionService.GetHistoricRatesAsync(historicRates));
        }
    }
}
