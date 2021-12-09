using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoDeSia.Data;
using ProjetoDeSia.Models;

namespace ProjetoDeSia.Controllers
{
    public class TecnicasController : Controller
    {
        private readonly ProjetoDeSiaContext _context;

        public TecnicasController(ProjetoDeSiaContext context)
        {
            _context = context;
        }

        // GET: Tecnicas
        public async Task<IActionResult> Index()
        {
            var projetoDeSiaContext = _context.Tecnica.Include(t => t.Utilizador);
            return View(await projetoDeSiaContext.ToListAsync());
        }

        // GET: Tecnicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnica = await _context.Tecnica
                .Include(t => t.Utilizador)
                .FirstOrDefaultAsync(m => m.IdTecnica == id);
            if (tecnica == null)
            {
                return NotFound();
            }

            return View(tecnica);
        }

        // GET: Tecnicas/Create
        public IActionResult Create()
        {
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizador, "IdUtilizador", "Email");
            return View();
        }

        // POST: Tecnicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTecnica,Nome,Descricao,UtilizadorId")] Tecnica tecnica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tecnica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizador, "IdUtilizador", "Email", tecnica.UtilizadorId);
            return View(tecnica);
        }

        // GET: Tecnicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnica = await _context.Tecnica.FindAsync(id);
            if (tecnica == null)
            {
                return NotFound();
            }
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizador, "IdUtilizador", "Email", tecnica.UtilizadorId);
            return View(tecnica);
        }

        // POST: Tecnicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTecnica,Nome,Descricao,UtilizadorId")] Tecnica tecnica)
        {
            if (id != tecnica.IdTecnica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tecnica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TecnicaExists(tecnica.IdTecnica))
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
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizador, "IdUtilizador", "Email", tecnica.UtilizadorId);
            return View(tecnica);
        }

        // GET: Tecnicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tecnica = await _context.Tecnica
                .Include(t => t.Utilizador)
                .FirstOrDefaultAsync(m => m.IdTecnica == id);
            if (tecnica == null)
            {
                return NotFound();
            }

            return View(tecnica);
        }

        // POST: Tecnicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tecnica = await _context.Tecnica.FindAsync(id);
            _context.Tecnica.Remove(tecnica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnicaExists(int id)
        {
            return _context.Tecnica.Any(e => e.IdTecnica == id);
        }
    }
}
