using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.IO;
using Microsoft.EntityFrameworkCore;
using sms_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace sms_project.Controllers
{
    public class SMSBashController : Controller
    {
        private readonly sms_projectContext _context;
        private IHostingEnvironment _hostingEnvironment;
        public SMSBashController(sms_projectContext context, IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            _context = context;
        }
        private Destinatario FromCsv(string csvLine)
        {
            string[] valores= csvLine.Split(';');
            Destinatario des = new Destinatario();
            des.ID = Convert.ToInt32(valores[0]);
            des.Nombre = valores[1];
            des.Apellido = valores[2];
            des.numero = valores[3];
            des.Nivel = valores[4];
            des.Grado = valores[5];
            des.Seccion = valores[6];
            return des;
        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            List <List<Destinatario>> lista_bash = new List<List<Destinatario>> ();
            foreach (var file in files)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                List<Destinatario> values = System.IO.File.ReadAllLines(filePath)
                                           .Skip(1)
                                           .Select(v => FromCsv(v))
                                           .ToList();
                
                lista_bash.Add(values);
                List<Destinatario> insert = new List<Destinatario> ();
                foreach (var val in values)
                {
                    if (!_context.Movie.Any(x => x==val))
                    {
                        insert.Add(val);
                    }
                }
                if (insert.Any())
                {
                    _context.AddRange(insert);
                    _context.SaveChanges();
                }
            }
            List<int>  lista_bash_send = new List<int> ();
            foreach (var list1 in lista_bash)
            {
                foreach (var desti in list1)
                {
                    lista_bash_send.Add(desti.ID);
                }
            }
            TempData ["lista_bash"] = lista_bash_send;
            return RedirectToAction("Index","SMS",new{list_ids = lista_bash_send});
        }
        public ActionResult Index()
        {
            
            return View();
        }
    }
}
