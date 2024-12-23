using AfricasTalkingCS;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using twilio_messager.Services;

namespace twilio_messager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NdaiMessageController(ILogger<NdaiMessageController> logger, TwilioService twilioService) : ControllerBase
    {
        private readonly ILogger<NdaiMessageController> _logger = logger;
        private readonly TwilioService _twilioService = twilioService;

        [HttpPost]
        public async Task<ActionResult> SendMessage(
            [FromQuery] string to,
            [FromQuery] string message
            )
        {
            var result = await _twilioService.SendMessage(to, message);
            return Ok(result);
        }

        [HttpPost("sms")]
        public async Task<ActionResult> SendMessageAT(
            [FromQuery] string to,
            [FromQuery] string message
            )
        {
            string UserName = Environment.GetEnvironmentVariable("APP_USER_NAME")
                         ?? throw new InvalidOperationException("APP_USER_NAME is not set in .env");
            string ApiKey = Environment.GetEnvironmentVariable("API_KEY")
                         ?? throw new InvalidOperationException("API_KEY is not set in .env");


            var gateway = new AfricasTalkingGateway(UserName, ApiKey);

            try
            {
                //var sms = gateway.SendMessage(recepients, msg);
                var sms = gateway.SendMessage(to, message);
                var sms1 = gateway.SendPremiumMessage(to, message, "shuledrive");
                Console.WriteLine(JsonConvert.SerializeObject(sms));
                Console.WriteLine(JsonConvert.SerializeObject(sms1));
                foreach (var res in sms["SMSMessageData"]["Recipients"])
                {
                    Console.WriteLine((string)res["number"] + ": ");
                    Console.WriteLine((string)res["status"] + ": ");
                    Console.WriteLine((string)res["messageId"] + ": ");
                    Console.WriteLine((string)res["cost"] + ": ");
                }
            }
            catch (AfricasTalkingGatewayException exception)
            {
                Console.WriteLine(exception);
            }

            // var result = await _twilioService.SendMessage(to, message);
            return Ok();
        }

    }
}
