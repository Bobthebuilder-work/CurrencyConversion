using Conversion.Domain.Models.Domain;
using Conversion.Domain.Models.DTO.ConversionIO;
using Conversion.Domain.Models.DTO.HistoricRatesIO;
using Conversion.Domain.Models.DTO.LatestExchangeIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion_Services.Interface
{
    public  interface ICurrencyConvertionService
    {
        Task<LatestExchangeRatesResponse> GetExchangeRatesAsync(LatestExchangeRatesRequest latestExchange);
        Task<ConvertResponse> ConvertAsync(ConvertRequest convertRequest);
        Task<HistoricRatesResponse> GetHistoricRatesAsync(HistoricRatesRequest historicRates);
    }
}
