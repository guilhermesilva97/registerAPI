using FirstIntegration.Application.Interface;
using FirstIntegration.Model.Common;
using FirstIntegration.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientApp _clientApp;
        public ClientController(IClientApp userApp)
        {
            _clientApp = userApp;
        }

        [HttpPost("insertClient")]
        [Authorize("XApiKey")]
        public async Task<IActionResult> InsertClient([FromBody] ClientRequest client)
        {
            ResultResponseObject<bool> resultResponse = await _clientApp.InsertClient(client);

            return Ok(resultResponse);
        }
    }
}
