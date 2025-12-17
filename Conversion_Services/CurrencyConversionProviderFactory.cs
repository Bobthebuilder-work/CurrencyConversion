using Conversion.Domain.Models.Domain;
using Conversion.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion_Services
{
    public class CurrencyConversionProviderFactory : ICurrencyConversionProviderFactory
    {
        private readonly IServiceProvider _conversionProvider;

        public CurrencyConversionProviderFactory(IServiceProvider serviceProvider)
        {
            _conversionProvider = serviceProvider;
        }
        ICurrencyConversionProvider ICurrencyConversionProviderFactory.GetProvider(CurrencyConversionProviderType currencyProvider)
        {
            throw new NotImplementedException();
        }
    }
}
