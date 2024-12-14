﻿using Microsoft.Extensions.Options;
using Twilio;
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
            var messageResource = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber("+16802196104"),
                to: new Twilio.Types.PhoneNumber("+254795600499")
            );

            Console.WriteLine(messageResource.Sid);
            return messageResource;
        }
    }
}