using Conversion.Domain.Models.DTO;
using Conversion_SharedServices.Services.Interfaces;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CurrencyConvertionHandling.Controllers
{
    [Route("api/v1/[controller]")]
    [EnableRateLimiting("AuthPolicy")]
    [ApiController]
    public class AuthNController : ControllerBase
    {
        private readonly IJWTGenerationService _jWTGeneration;

        private IDictionary<string, string> users;
        public AuthNController(IJWTGenerationService jWTGeneration)
        {
            _jWTGeneration = jWTGeneration;

            //mockData
            users = new Dictionary<string, string>()
            {
                { "admin","admin123" },
                {"user","user123" }
            };
        }

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] LoginRequest request)
        {
            if(!users.ContainsKey(request.userName))
                return Unauthorized();

            if (users[request.userName] != request.password)
                return Unauthorized();

            var (email,role) = request.userName == "admin" ? ("admin@currency.com","admin") : ("user@currenct.com","user");

            var token = _jWTGeneration.GenerateToken(
                email: email,role);

            return Ok(token);
        }
    }
}
