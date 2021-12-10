using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoDeSia.Data;
using ProjetoDeSia.Models;

namespace ProjetoDeSia.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ProjetoDeSiaContext _context;

        public ItemsController(ProjetoDeSiaContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var projetoDeSiaContext = _context.Item.Include(i => i.Tecnica);
            return View(await projetoDeSiaContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Tecnica)
                .FirstOrDefaultAsync(m => m.IdItem == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["TecnicaId"] = new SelectList(_context.Tecnica, "IdTecnica", "Descricao");
            //view data com os nomes dos quadrantes

            List<Quadrante> listaTodosQuad = new List<Quadrante>();

            foreach (Quadrante item in _context.Quadrante.ToList())
            {
                if (item.TecnicaId == HttpContext.Session.GetInt32("TecnicaId"))
                {
                    //remover da lista
                    listaTodosQuad.Add(item);
                }
            }
            ViewData["QuadId"] = new SelectList(listaTodosQuad, "IdQuadrante", "Nome_Quad");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdItem,Descricao,QuadId,Importancia")] Item item)
        {
            //no criar item vai ter de estar associado ao item um quadrante
            if (ModelState.IsValid)
            {
                //gerar automaticamente:
                //Pontuação que é 0-1-2-3-4
                if(item.Importancia == 0)
                {
                    item.Pontucao = 0;
                }
                if (item.Importancia == 1)
                {
                    item.Pontucao = 2.5;
                }
                if (item.Importancia == 2)
                {
                    item.Pontucao = 5;
                }
                if (item.Importancia == 3)
                {
                    item.Pontucao = 7.5;
                }
                if (item.Importancia == 4)
                {
                    item.Pontucao = 10;
                }
                //Classificaçao - nome do quadrante que esta associado
                Quadrante q = _context.Quadrante.SingleOrDefault(q => q.IdQuadrante == item.QuadId);
                if (q != null)
                {
                    item.classificacao = q.Nome_Quad;
                }
                //TecnicaID
                item.TecnicaId = (int)HttpContext.Session.GetInt32("TecnicaId");
                

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TecnicaId"] = new SelectList(_context.Tecnica, "IdTecnica", "Descricao", item.TecnicaId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["TecnicaId"] = new SelectList(_context.Tecnica, "IdTecnica", "Descricao", item.TecnicaId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdItem,Descricao,Pontucao,Importancia,classificacao,TecnicaId,QuadId")] Item item)
        {
            if (id != item.IdItem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.IdItem))
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
            ViewData["TecnicaId"] = new SelectList(_context.Tecnica, "IdTecnica", "Descricao", item.TecnicaId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Tecnica)
                .FirstOrDefaultAsync(m => m.IdItem == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.IdItem == id);
        }
    }
}
