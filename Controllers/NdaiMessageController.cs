using Microsoft.AspNetCore.Mvc;
using twilio_messager.Services;

namespace twilio_messager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NdaiMessageController : ControllerBase
    {
        private readonly ILogger<NdaiMessageController> _logger;
        private readonly TwilioService _twilioService;

        public NdaiMessageController(ILogger<NdaiMessageController> logger, TwilioService twilioService)
        {
            _logger = logger;
            _twilioService = twilioService;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(
            [FromQuery] string to,
            [FromQuery] string message
            )
        {
            var result = await _twilioService.SendMessage(to, message);
            return Ok(result);
        }
    }
}
