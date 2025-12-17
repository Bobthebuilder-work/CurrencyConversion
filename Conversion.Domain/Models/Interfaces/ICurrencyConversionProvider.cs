using Conversion.Domain.Models.DTO;
using Conversion.Domain.Models.DTO.ConversionIO;
using Conversion.Domain.Models.DTO.HistoricRatesIO;
using Conversion.Domain.Models.DTO.LatestExchangeIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.Interfaces
{
    public interface ICurrencyConversionProvider
    {
        string Name { get; }
        //Task<decimal> ConvertAsync(string source, string target, decimal amount);
        //Task<PagenatedResult<ExchangeRate>> HistoricExchangeRates(DateTime startDate,DateTime endDate, string baseCurrency);
        //Task<IDictionary<string,decimal>> GetExchangeRate(string baseCurrency);

        Task<LatestExchangeRatesResponse> GetExchangeRatesAsync(LatestExchangeRatesRequest latestExchange);
        Task<ConvertResponse> ConvertAsync(ConvertRequest convertRequest);
        Task<HistoricRatesResponse> GetHistoricRatesAsync(HistoricRatesRequest historicRates);

    }
}
