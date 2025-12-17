using Conversion.Domain.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion_SharedServices.Services.Interfaces
{
    public interface IJWTGenerationService
    {
        TokenResponse GenerateToken(string email, string role);
    }
}
