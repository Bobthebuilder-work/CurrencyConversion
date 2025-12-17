using Conversion.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.DTO.LatestExchangeIO
{
    public class LatestExchangeRatesRequest
    {
        [JsonPropertyName("base")]
        public string baseCurrency { get; set; } = "EUR";
        public string[]? symbols { get; set; }
        public CurrencyConversionProviderType conversionProvider { get; set; }
    }
}
