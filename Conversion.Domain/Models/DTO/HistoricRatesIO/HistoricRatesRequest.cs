using Conversion.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.DTO.HistoricRatesIO
{
    public class HistoricRatesRequest
    {
        public string? baseCurrency { get; set; } = "EUR";
        public string? targetCurrency { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;

        public CurrencyConversionProviderType conversionProvider { get; set; }
    }
}
