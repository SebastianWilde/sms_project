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
        
        private readonly sms_projectContext _context;
        public SMSController(sms_projectContext context)
        {
            _context = context;
        }
        public List<string> getNumeros()
        {
            var test = from s in _context.Movie
                       orderby s.numero
                       select s.numero;
            return test.ToList();
        }
        public IActionResult Index()
        {
            // Your Account SID from twilio.com/console

            //Agreguen su Credenciales de Twilion
            var accountSid = "ACe015558675ea7b8edda3f40da3eb28e4";
            // Your Auth Token from twilio.com/console
            var authToken = "3ce3d327d64356a8a1ff5de89f5ebf43";   

            TwilioClient.Init(accountSid, authToken);

            
            var people = new Dictionary<string, string>() {
                {"+51959741515", "Sebas"},
                {"+51972892866", "Wilbur"}
            };
            /*var phoneNumber = new PhoneNumber("+51959741515");
            var validationRequest = ValidationRequestResource.Create(
                phoneNumber,
                friendlyName: "Sebas Number");
            
            Console.WriteLine(validationRequest.ValidationCode);
            */
            // Iterate over all our friends
            foreach (var person in people)
            {
                // Send a new outgoing SMS by POSTing to the Messages resource
                MessageResource.Create(
                    from: new PhoneNumber("+15042245074"), // From number, must be an SMS-enabled Twilio number
                    to: new PhoneNumber(person.Key), // To number, if using Sandbox see note above
                                                     // Message content
                    body: $"Hey {person.Value} Monkey Party at 6PM. Bring Bananas!");

                Console.WriteLine($"Sent message to {person.Value}");
            }
            ViewData["Message"] = "Mensaje Enviado";
            return View();
        }
    }
}
