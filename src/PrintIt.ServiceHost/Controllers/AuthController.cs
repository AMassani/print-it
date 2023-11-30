using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using PrintIt.ServiceHost.Dto;

namespace PrintIt.ServiceHost.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static readonly string ALLOWED_CLIENTS_SECTION = "AllowedClients";
        private static readonly string ALLOWED_CLIENTS_KEY = "ClientId";

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("token")]
        public async Task<IActionResult> GetSessionTokenAsync(string clientKey)
        {
            var clientIdConfigValue = _configuration.GetSection(ALLOWED_CLIENTS_SECTION).Get<List<AllowedClients>>();

            var clientIdKeyValue = clientIdConfigValue.FirstOrDefault(x => x.ClientId == clientKey);
            if (clientIdKeyValue == null)
            {
                return NotFound();
            }
            var key = clientIdKeyValue.Key;
            var encryptedValue = AESEncryptionUtility.Encrypt(clientKey, key);
            return await Task.FromResult(new JsonResult(encryptedValue));
        }

    }
}
