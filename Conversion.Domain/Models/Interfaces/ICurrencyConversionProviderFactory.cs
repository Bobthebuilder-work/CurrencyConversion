using Conversion.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.Interfaces
{
    public interface ICurrencyConversionProviderFactory
    {
        ICurrencyConversionProvider GetProvider(CurrencyConversionProviderType currencyProvider);
    }
}
