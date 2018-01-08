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
            var accountSid = "AC9f96b28edbb32a3c006942d55e075e62";
            // Your Auth Token from twilio.com/console
            var authToken = "3e29e0079c3303d6b7d67e364b56700e";   

            TwilioClient.Init(accountSid, authToken);

            
            var people = getNumeros();
            // Iterate over all our friends
            foreach (var person in people)
            {
                // Send a new outgoing SMS by POSTing to the Messages resource
                /*MessageResource.Create(
                    from: new PhoneNumber("+15013024007"), // From number, must be an SMS-enabled Twilio number
                    to: new PhoneNumber(person.Key), // To number, if using Sandbox see note above
                                                     // Message content
                    body: $"Hey {person.Value} Monkey Party at 6PM. Bring Bananas!");
                */
                Console.WriteLine("Enviando a Numero: {0}",person);
            }
            ViewData["Message"] = "Mensaje Enviado";
            return View();
        }
    }
}
