using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterAPI.Application.Interface;
using RegisterAPI.Entity.Logger;
using RegisterAPI.Model.Common;

namespace RegisterAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : Controller
    {
        private readonly ILoggerApp _loggerApp;
        public LoggerController(ILoggerApp loggerApp)
        {
            _loggerApp = loggerApp;
        }

        [HttpGet]
        [Authorize("XApiKey")]
        public async Task<IActionResult> GettAllLogs()
        {
            ResultResponseObject<IEnumerable<Log>> resultResponse = await _loggerApp.GetAllLogs();

            return Ok(resultResponse);
        }
    }
}
