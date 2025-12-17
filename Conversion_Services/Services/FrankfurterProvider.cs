using Conversion.Domain.Models.Domain;
using Conversion.Domain.Models.DTO;
using Conversion.Domain.Models.DTO.ConversionIO;
using Conversion.Domain.Models.DTO.HistoricRatesIO;
using Conversion.Domain.Models.DTO.LatestExchangeIO;
using Conversion.Domain.Models.Interfaces;
using Conversion_SharedServices.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Conversion_Services.Services
{
    public class FrankfurterProvider : ICurrencyConversionProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        public FrankfurterProvider(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _cache = memoryCache;
        }

        public string Name => CurrencyConversionProviderType.Frankfurter.ToString();

        public async Task<ConvertResponse> ConvertAsync(ConvertRequest convertRequest)
        {
            var cacheKey = $"convert_{convertRequest.fromCurrency}_{convertRequest.toCurrency}_{convertRequest.amount}";
            if (_cache.TryGetValue(cacheKey, out ConvertResponse cached))
                return cached;

            var json = await _httpClient.GetFromJsonAsync<JsonElement>(
                $"?base={convertRequest.fromCurrency}&symbols={convertRequest.toCurrency}");

            var value = json.GetProperty("rates").GetProperty(convertRequest.toCurrency).GetDecimal();

            var result = new ConvertResponse
            {
                rate = value,
                result = value * convertRequest.amount
            };

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            return result;
        }

        public async Task<LatestExchangeRatesResponse> GetExchangeRatesAsync(LatestExchangeRatesRequest latestExchangeRates)
        {
            var symbols = latestExchangeRates.symbols != null
            ? $"&symbols={string.Join(",", latestExchangeRates.symbols)}"
            : string.Empty;

            var cacheKey = $"latest_{latestExchangeRates.baseCurrency}_{symbols}";
            if (_cache.TryGetValue(cacheKey, out LatestExchangeRatesResponse cached))
                return cached;

            var response = await _httpClient.GetFromJsonAsync<LatestExchangeRatesResponse>(
                $"?base={latestExchangeRates.baseCurrency}{symbols}");

            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
            return response;
        }

        public async Task<HistoricRatesResponse> GetHistoricRatesAsync(HistoricRatesRequest historicRates)
        {
            var cacheKey = $"historic_{historicRates.baseCurrency}_{historicRates.startDate:yyyyMMdd}_{historicRates.endDate:yyyyMMdd}_{historicRates.targetCurrency??string.Empty}";
            if (!_cache.TryGetValue(cacheKey, out Dictionary<DateTime, IDictionary<string, decimal>> allRates))
            {
                string query = $"{historicRates.startDate:yyyy-MM-dd}..{historicRates.endDate:yyyy-MM-dd}?base={historicRates.baseCurrency}";
                query+= historicRates.targetCurrency != null ? $"&symbols={historicRates.targetCurrency}" : string.Empty;
                var json = await _httpClient.GetFromJsonAsync<JsonElement>(query);

                    allRates = json.GetProperty("rates")
                        .EnumerateObject()
                        .ToDictionary(
                            x => DateTime.Parse(x.Name),
                            x => (IDictionary<string, decimal>)
                                x.Value
                                    .EnumerateObject()
                                    .ToDictionary(
                                        currency => currency.Name,
                                        currency => currency.Value.GetDecimal()
                                    )
                        );


                _cache.Set(cacheKey, allRates, TimeSpan.FromMinutes(30));
            }
            var total = allRates.Count;
            var pagedRates = allRates
                .OrderByDescending(x => x.Key)
                .Skip((historicRates.page - 1) * historicRates.pageSize)
                .Take(historicRates.pageSize)
                .ToDictionary(x => x.Key, x => x.Value);

            return new HistoricRatesResponse
            {
                page = historicRates.page,
                pageSize = historicRates.pageSize,
                totalRecords = total,
                rates = pagedRates
            };
        }
    }
}
