using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.EntityFrameworkCore;

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
        
        public async Task<IActionResult> Index(Mensaje_Destinatarios search, List<int> list_ids = null) 
        {
            Helpers.Queries queries = new Helpers.Queries(_context);
            var movies = from m in _context.Movie
                        select m;
            if(search != null)
            {
                if(!String.IsNullOrEmpty(search.NombreQ))
                {
                    movies = movies.Where(s => s.Nombre.Contains(search.NombreQ));
                }
                if (!String.IsNullOrEmpty(search.NivelQ))
                {
                    movies = movies.Where(s => s.Nivel.Contains(search.NivelQ));
                }
                if (!String.IsNullOrEmpty(search.GradoQ))
                {
                    movies = movies.Where(s => s.Grado.Contains(search.GradoQ));
                }
                if (!String.IsNullOrEmpty(search.SeccionQ))
                {
                    movies = movies.Where(s => s.Seccion.Contains(search.SeccionQ));
                }
            }
            var queryList = new Mensaje_Destinatarios();
            if (list_ids.Any())
            {
                var aux = await movies.Where(s => list_ids.Contains(s.ID)).ToListAsync();
                queryList.Lista = aux;
            }
            else
            {
                queryList.Lista = await movies.ToListAsync();
            }
            queryList.Select = new  Seleccionable();
            queryList.Select.Niveles = queries.getValoresNivel();
            queryList.Select.Grados = queries.getValoresGrado();
            queryList.Select.Secciones = queries.getValoresSeccion();
            return View(queryList);
        }


        [HttpPost]
        public IActionResult Detalles(Mensaje_Destinatarios Forma)
        {

            List<string> destiatarios= new List<string>();
            if(Forma.Lista != null)
            {
                for (int i = 0; i < Forma.Lista.Count(); i++)
                {
                    destiatarios.Add(Forma.Lista[i].numero);
                }
                Helpers.MassiveSms serviceMassiveSms = new Helpers.MassiveSms();

                serviceMassiveSms.sendMassiveSms(destiatarios,Forma.Mensaje);
            }
            var que = from r in _context.Movie select r;
        
            foreach (Destinatario item in que)
            {
                _context.Movie.Remove(item);
            }
            _context.SaveChangesAsync();
            ViewData["Message"] = "Mensaje Enviado";
            return View();
        }
    }
}