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
                _context.AddRange(values);
                _context.SaveChanges();
            }
            return View();
            /*long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                        var mensaje=string.Empty;
                        using(StreamReader reader= new StreamReader(formFile.OpenReadStream()))
                        {
                            mensaje=reader.ReadToEnd();
                        }
                        Console.WriteLine(mensaje);
                    }
                }
            }

            return Ok(new { count = files.Count, size, filePath });*/
        }
        public ActionResult Index()
        {
            
            return View();
        }
    }
}
