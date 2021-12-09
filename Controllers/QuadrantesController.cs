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
    public class QuadrantesController : Controller
    {
        private readonly ProjetoDeSiaContext _context;

        public QuadrantesController(ProjetoDeSiaContext context)
        {
            _context = context;
        }

        // GET: Quadrantes
        public async Task<IActionResult> Index()
        {
            var projetoDeSiaContext = _context.Quadrante.Include(q => q.Tecnica);
            return View(await projetoDeSiaContext.ToListAsync());
        }

        // GET: Quadrantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quadrante = await _context.Quadrante
                .Include(q => q.Tecnica)
                .FirstOrDefaultAsync(m => m.IdQuadrante == id);
            if (quadrante == null)
            {
                return NotFound();
            }

            return View(quadrante);
        }

        // GET: Quadrantes/Create
        public IActionResult Create()
        {
            ViewData["TecnicaId"] = new SelectList(_context.Tecnica, "IdTecnica", "Descricao");
            return View();
        }

        // POST: Quadrantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdQuadrante,Nome_Quad,PosicaoQuadrante,TecnicaId")] Quadrante quadrante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quadrante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TecnicaId"] = new SelectList(_context.Tecnica, "IdTecnica", "Descricao", quadrante.TecnicaId);
            return View(quadrante);
        }

        // GET: Quadrantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quadrante = await _context.Quadrante.FindAsync(id);
            if (quadrante == null)
            {
                return NotFound();
            }
            ViewData["TecnicaId"] = new SelectList(_context.Tecnica, "IdTecnica", "Descricao", quadrante.TecnicaId);
            return View(quadrante);
        }

        // POST: Quadrantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdQuadrante,Nome_Quad,PosicaoQuadrante,TecnicaId")] Quadrante quadrante)
        {
            if (id != quadrante.IdQuadrante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quadrante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuadranteExists(quadrante.IdQuadrante))
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
            ViewData["TecnicaId"] = new SelectList(_context.Tecnica, "IdTecnica", "Descricao", quadrante.TecnicaId);
            return View(quadrante);
        }

        // GET: Quadrantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quadrante = await _context.Quadrante
                .Include(q => q.Tecnica)
                .FirstOrDefaultAsync(m => m.IdQuadrante == id);
            if (quadrante == null)
            {
                return NotFound();
            }

            return View(quadrante);
        }

        // POST: Quadrantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quadrante = await _context.Quadrante.FindAsync(id);
            _context.Quadrante.Remove(quadrante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuadranteExists(int id)
        {
            return _context.Quadrante.Any(e => e.IdQuadrante == id);
        }
    }
}
