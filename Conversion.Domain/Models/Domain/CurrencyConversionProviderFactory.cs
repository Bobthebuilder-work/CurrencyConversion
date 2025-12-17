using Conversion.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.Domain
{
    public class CurrencyConversionProviderFactory : ICurrencyConversionProviderFactory
    {
        private readonly IEnumerable<ICurrencyConversionProvider> _currencyConversionProvider;
        public CurrencyConversionProviderFactory(IEnumerable<ICurrencyConversionProvider> currencyConversions) {
            _currencyConversionProvider = currencyConversions;
        } 
        public ICurrencyConversionProvider GetProvider(CurrencyConversionProviderType currencyProvider)
        {
            return _currencyConversionProvider.FirstOrDefault(x => x.Name.Equals(currencyProvider.ToString(), StringComparison.OrdinalIgnoreCase))?? throw new NotImplementedException($"{currencyProvider} not supported");
        }
    }
}
