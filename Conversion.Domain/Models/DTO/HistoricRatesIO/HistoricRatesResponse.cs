using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.DTO.HistoricRatesIO
{
    public class HistoricRatesResponse
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public Dictionary<DateTime,IDictionary<string,decimal>> rates { get; set; }
    }
}
