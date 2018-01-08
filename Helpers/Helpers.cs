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

namespace sms_project.Helpers
{
    public class Queries
    {
        private readonly sms_projectContext _context;

        public Queries (sms_projectContext context)
        {
            _context = context;
        }
        
        public List<string> getNumerosFromUsuarios()
        {
            var test = from s in _context.Movie
            orderby s.numero 
            select s.numero;
            System.Console.WriteLine(test);
            return test.ToList();
        }
    }

    public class MassiveSms
    {
        private string accountSid;
        private string authToken;
        private string ToPhoneNumber;

        public MassiveSms()
        {
            accountSid = "ACf1b4d0ced4c84a4263bdfd51d84b5680";
            authToken = "caaa7a05773f0e14eab6b875148b296c";
            ToPhoneNumber = "+12282542751";
        }

        public bool sendMassiveSms(List<string> fromNumbers,string mensaje)
        {
            TwilioClient.Init(accountSid, authToken);
            List <string> enviados = new List<string>();
            List <string> noEnviados = new List<string>();
            foreach(var destinatario in fromNumbers)
            {
                try
                {
                    var message = MessageResource.Create(
                    to: new PhoneNumber(destinatario),
                    from: new PhoneNumber(ToPhoneNumber),
                    body: mensaje);
                    enviados.Add(destinatario);
                }
                catch 
                {
                    noEnviados.Add(destinatario);

                }
            }

            System.Console.WriteLine("Enviados \n-------------------");
            foreach (var aux in enviados)
            {
                System.Console.WriteLine($"{aux}\n");
            }
            System.Console.WriteLine("No Enviados \n-------------------");
            foreach (var aux in noEnviados)
            {
                System.Console.WriteLine($"{aux}\n");
            }

            return true;
        }
    }
}