using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sms_project.Models;

namespace sms_project.Controllers
{
    public class DestinatarioController : Controller
    {
        private readonly sms_projectContext _context;

        public DestinatarioController(sms_projectContext context)
        {
            _context = context;
        }

        // GET: Destinatario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Destinatario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinatario = await _context.Movie
                .SingleOrDefaultAsync(m => m.ID == id);
            if (destinatario == null)
            {
                return NotFound();
            }

            return View(destinatario);
        }

        // GET: Destinatario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destinatario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Apellido,numero,Nivel,Grado,Seccion")] Destinatario destinatario)
        {
            if (ModelState.IsValid)
            {
                _context.Movie.Add(destinatario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(destinatario);
        }

        // GET: Destinatario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinatario = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            if (destinatario == null)
            {
                return NotFound();
            }
            return View(destinatario);
        }

        // POST: Destinatario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Apellido,Nivel,Grado,Seccion")] Destinatario destinatario)
        {
            if (id != destinatario.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destinatario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinatarioExists(destinatario.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(destinatario);
        }

        // GET: Destinatario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinatario = await _context.Movie
                .SingleOrDefaultAsync(m => m.ID == id);
            if (destinatario == null)
            {
                return NotFound();
            }

            return View(destinatario);
        }

        // POST: Destinatario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destinatario = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            _context.Movie.Remove(destinatario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinatarioExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
