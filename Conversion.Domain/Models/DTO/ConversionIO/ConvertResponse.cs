using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.DTO.ConversionIO
{
    public class ConvertResponse
    {
        public decimal result { get; set; }
        public decimal rate { get; set; }
    }
}
