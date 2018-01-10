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

        public List<string> getValoresNivel()
        {
            List <string> ValoresNivel = new List<string>();
            var test = _context.Movie.OrderBy(p => p.Nivel).ToLookup(p=>p.Nivel);
            foreach( var aux in test)
            {
                ValoresNivel.Add(aux.Key);
            }
            return ValoresNivel;
        }
        public List<string> getValoresGrado()
        {
            List <string> ValoresGrado = new List<string>();
            var test = _context.Movie.OrderBy(p => p.Grado).ToLookup(p=>p.Grado);
            foreach( var aux in test)
            {
                ValoresGrado.Add(aux.Key);
            }
            return ValoresGrado;
        }
        public List<string> getValoresSeccion()
        {
            List <string> ValoresSeccion = new List<string>();
            var test = _context.Movie.OrderBy(p => p.Seccion).ToLookup(p=>p.Seccion);
            foreach( var aux in test)
            {
                ValoresSeccion.Add(aux.Key);
            }
            return ValoresSeccion;
        }
    }

    public class MassiveSms
    {
        private string accountSid;
        private string authToken;
        private string ToPhoneNumber;

        public MassiveSms()
        {
            accountSid = "ACe015558675ea7b8edda3f40da3eb28e4";
            authToken = "3ce3d327d64356a8a1ff5de89f5ebf43";
            ToPhoneNumber = "+15042245074";
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