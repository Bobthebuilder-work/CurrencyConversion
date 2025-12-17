using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.DTO.LatestExchangeIO
{
    public class LatestExchangeRatesResponse
    {
        [JsonPropertyName("base")]
        public string baseCurrency { get; set; }
        public DateTime date { get; set; }
        public Dictionary<string, decimal> rates { get; set; }
    }
}
