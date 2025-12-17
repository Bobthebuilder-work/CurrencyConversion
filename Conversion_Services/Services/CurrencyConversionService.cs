using Conversion.Domain.Models.Domain;
using Conversion.Domain.Models.DTO.ConversionIO;
using Conversion.Domain.Models.DTO.HistoricRatesIO;
using Conversion.Domain.Models.DTO.LatestExchangeIO;
using Conversion.Domain.Models.Interfaces;
using Conversion_Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion_Services.Services
{
    public class CurrencyConversionService : ICurrencyConvertionService
    {
        private readonly ICurrencyConversionProviderFactory _currencyConversionProviderFactory;
        public CurrencyConversionService(ICurrencyConversionProviderFactory currencyConversionProvider)
        {
            _currencyConversionProviderFactory = currencyConversionProvider;
        }
        public async Task<ConvertResponse> ConvertAsync(ConvertRequest convertRequest)
        {
            var rateProvider = _currencyConversionProviderFactory.GetProvider(convertRequest.conversionProvider);
            return await rateProvider.ConvertAsync(convertRequest);
        }

        public async Task<LatestExchangeRatesResponse> GetExchangeRatesAsync(LatestExchangeRatesRequest latestExchange)
        {
            var exchangeRate = _currencyConversionProviderFactory.GetProvider(latestExchange.conversionProvider);
            return await exchangeRate.GetExchangeRatesAsync(latestExchange);
        }

        public async Task<HistoricRatesResponse> GetHistoricRatesAsync(HistoricRatesRequest historicRates)
        {
            var historicRate = _currencyConversionProviderFactory.GetProvider(historicRates.conversionProvider);
            return await historicRate.GetHistoricRatesAsync(historicRates);
        }
    }
}
