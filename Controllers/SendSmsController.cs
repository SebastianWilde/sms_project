using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sms_project.Models;

namespace sms_project.Controllers
{
    public class SendSmsController : Controller 
    {
        private readonly sms_projectContext _context;

        public SendSmsController(sms_projectContext context)
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

        // GET: /SendSms
        public IActionResult Index()
        {
            
            var t = getNumeros();
            foreach (var aux in t)
            {
                Console.WriteLine(aux);
            }
            return View();
        }
    }
}