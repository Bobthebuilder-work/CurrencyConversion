using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.Domain
{
    public class ExchangeRateResponse
    {
        public decimal amount { get; set; }
        public string Base { get; set; }
        public DateTime date { get; set; }
        public Dictionary<string,decimal> rates { get; set; }
    }
}
