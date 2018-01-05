using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using sms_project.Models;

namespace sms_project.Controllers
{
    public class SMSController : Controller
    {
        public IActionResult Index()
        {
            // Your Account SID from twilio.com/console
            var accountSid = "AC9f96b28edbb32a3c006942d55e075e62";
            // Your Auth Token from twilio.com/console
            var authToken = "3e29e0079c3303d6b7d67e364b56700e";   

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber("+51972892866"),
                from: new PhoneNumber("+15013024007"),
                body: "Hello from C#");

            Console.WriteLine(message.Sid);
            Console.Write("Press any key to continue.");
            Console.ReadKey();
            ViewData["Message"] = "Mensaje Enviado";
            return View();
        }
    }
}
