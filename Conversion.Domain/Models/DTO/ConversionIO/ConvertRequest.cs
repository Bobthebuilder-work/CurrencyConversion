using Conversion.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.DTO.ConversionIO
{
    public class ConvertRequest
    {
        public string fromCurrency { get; set; }
        public string toCurrency { get; set; }
        public decimal amount { get; set; }
        public CurrencyConversionProviderType conversionProvider { get; set; }
    }
}
