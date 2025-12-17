using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.DTO
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
