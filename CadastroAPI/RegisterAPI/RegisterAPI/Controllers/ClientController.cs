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
    }
}
