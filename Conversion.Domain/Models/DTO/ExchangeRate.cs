using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.DTO
{
    public class ExchangeRate
    {
        public string sourceCurrency { get; set; }
        public string targetCurrency { get; set; }
        public decimal conversionRate { get; set; }
        public DateTime exchangeDate { get; set; }
    }
}
