using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterAPI.Application.Interface;
using RegisterAPI.Model.Common;
using RegisterAPI.Model.Request.User;

namespace RegisterAPI.API.Controllers
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

        [HttpPost]
        [Authorize("XApiKey")]
        public async Task<IActionResult> InsertClient([FromBody] ClientRequest request)
        {
            ResultResponseObject<Guid> resultResponse = await _clientApp.InsertClient(request);

            if (resultResponse.Success)
            {
                return Ok(resultResponse);
            }
            else
            {
                return BadRequest(resultResponse);
            }
        }

        [HttpPost("syncClients")]
        [Authorize("XApiKey")]
        public async Task<IActionResult> SyncClients(Guid integrationGuid)
        {
            ResultResponseObject<bool> resultResponse = await _clientApp.SyncClients(integrationGuid);

            if (resultResponse.Success)
            {
                return Ok(resultResponse);
            }
            else
            {
                return BadRequest(resultResponse);
            }
        }
    }
}
