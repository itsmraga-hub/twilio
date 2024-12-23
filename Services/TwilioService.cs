using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using twilio_messager.Models;

namespace twilio_messager.Services
{
    public class TwilioService
    {
        private readonly TwilioSettings _settings;

        public TwilioService(IOptions<TwilioSettings> options)
        {
            _settings = options.Value;
            InitializeTwilio();
        }

        private void InitializeTwilio()
        {
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
        }

        public async Task<MessageResource> SendMessage(string to, string message)
        {
            //try
            //{
                var messageResource = await MessageResource.CreateAsync(
                    body: message,
                    from: new Twilio.Types.PhoneNumber("+16802196104"),
                    to: new Twilio.Types.PhoneNumber(to)
                );

                Console.WriteLine(messageResource.Sid);
                return messageResource;
            //}
            //catch (ApiException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return null!;
            //}
        }
    }
}
