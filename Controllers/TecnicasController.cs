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
            ViewData["UtilizadorId"] = new SelectList(_context.Utilizador, "IdUtilizador");
            return View();
        }

        // POST: Tecnicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTecnica,Nome,Descricao")] Tecnica tecnica)
        {
            if (ModelState.IsValid && HttpContext.Session.GetString("UtilizadorId") != null)
            {
                tecnica.UtilizadorId = Convert.ToInt32(HttpContext.Session.GetString("UtilizadorId"));
                _context.Add(tecnica);

                //quando se cria uma tecnica vai-se criar os 4 quadrantes, pois uma tecnica tem sempre 4 quadrantes
                Quadrante quadrante1 = new Quadrante();
                Quadrante quadrante2 = new Quadrante();
                Quadrante quadrante3 = new Quadrante();
                Quadrante quadrante4 = new Quadrante();

                //--
                quadrante1.TecnicaId = tecnica.IdTecnica;
                quadrante2.TecnicaId = tecnica.IdTecnica;
                quadrante3.TecnicaId = tecnica.IdTecnica;
                quadrante4.TecnicaId = tecnica.IdTecnica;

                //--
                quadrante1.Tecnica = tecnica;
                quadrante2.Tecnica = tecnica; 
                quadrante3.Tecnica = tecnica;
                quadrante4.Tecnica = tecnica;

                //---
                quadrante1.PosicaoQuadrante = 1;
                quadrante2.PosicaoQuadrante = 2;
                quadrante3.PosicaoQuadrante = 3;
                quadrante4.PosicaoQuadrante = 4;

                //---
                quadrante1.Nome_Quad = "Quadrante1";
                quadrante2.Nome_Quad = "Quadrante2";
                quadrante3.Nome_Quad = "Quadrante3";
                quadrante4.Nome_Quad = "Quadrante4";


                //---

                _context.Add(quadrante1);
                _context.Add(quadrante2);
                _context.Add(quadrante3);
                _context.Add(quadrante4);

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

            //quando se apaga uma tecnica tem de se apagar da base de dados todos os itens e quadrantes que pertencem a essa tecnia
            //apagar itens
            foreach (Item item in _context.Item.ToList())
            {
                if (item.TecnicaId == id)
                {
                    //apagar os itens
                    _context.Item.Remove(item);

                }
            }
            //apagar quadrantes
            foreach (Quadrante quad in _context.Quadrante.ToList())
            {
                if (quad.TecnicaId == id)
                {
                    //apagar os itens
                    _context.Quadrante.Remove(quad);

                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TecnicaExists(int id)
        {
            return _context.Tecnica.Any(e => e.IdTecnica == id);
        }


        
       //---------------------Minhas Funções------------------------------

        public async Task<IActionResult> EntrarTecnica(int? id) //id da tecnica
        {
    
            UsarTecnicaViewModel usarTecnicaViewModel = new UsarTecnicaViewModel();
            Tecnica tecnica = await _context.Tecnica.FirstOrDefaultAsync(m => m.IdTecnica == id);

            //verificar se o id da tecnica pertence ao id do utilizador logado      
            if (tecnica != null && tecnica.UtilizadorId.ToString() == HttpContext.Session.GetString("UtilizadorId"))
            {
                usarTecnicaViewModel.oTecnica = tecnica;
                //meter a view vag do id da tecnica que esta aberta
                ViewBag.IdTecnica = tecnica.IdTecnica;
                HttpContext.Session.SetInt32("TecnicaId", tecnica.IdTecnica);
               

                //se o id da tecnica for igual ao do utilizador tem permições para aceder a pagina
                usarTecnicaViewModel.temPermicoes = true;

                //preencher a lista de quadrantes
                usarTecnicaViewModel.oListQuadrante = _context.Quadrante.ToList();

                //preencher a lista dos itens
                usarTecnicaViewModel.oListItem = _context.Item.ToList();
            }
            else
            {
                usarTecnicaViewModel.temPermicoes = false;
            }

            return View(usarTecnicaViewModel);

        }


    }
}
