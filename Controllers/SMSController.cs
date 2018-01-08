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
        public IActionResult Index()
        {
            // Helpers.Queries queries =  new Helpers.Queries(_context);
            // List<string> destiatarios = queries.getNumerosFromUsuarios();
            
            // Helpers.MassiveSms serviceMassiveSms = new Helpers.MassiveSms();

            // string mensaje  = "Esto es una prueba";
            // serviceMassiveSms.sendMassiveSms(destiatarios,mensaje);
            // ViewData["Message"] = "Mensaje Enviado";
            return View();
        }

        [HttpPost]
        public IActionResult Detalles(string Mensaje)
        {
            System.Console.WriteLine(Mensaje);
            Helpers.Queries queries =  new Helpers.Queries(_context);
            List<string> destiatarios = queries.getNumerosFromUsuarios();
            
            Helpers.MassiveSms serviceMassiveSms = new Helpers.MassiveSms();

            serviceMassiveSms.sendMassiveSms(destiatarios,Mensaje);
            ViewData["Message"] = "Mensaje Enviado";
            return View();
        }
    }
}
