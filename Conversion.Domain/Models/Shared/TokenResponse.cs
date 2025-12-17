using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Domain.Models.Shared
{
    public class TokenResponse
    {
        public string accessToken { get; set; }
        public DateTime expiresAt { get; set; }
    }
}
