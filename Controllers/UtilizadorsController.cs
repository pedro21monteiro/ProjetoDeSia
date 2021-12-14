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
    public class UtilizadorsController : Controller
    {
        private readonly ProjetoDeSiaContext _context;

        public UtilizadorsController(ProjetoDeSiaContext context)
        {
            _context = context;
        }

        // GET: Utilizadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilizador.ToListAsync());
        }

        // GET: Utilizadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizador
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // GET: Utilizadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilizadors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUtilizador,UserName,Password,Email,Categoria")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilizador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilizador);
        }

        // GET: Utilizadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizador.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUtilizador,UserName,Password,Email,Categoria")] Utilizador utilizador)
        {
            if (id != utilizador.IdUtilizador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.IdUtilizador))
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
            return View(utilizador);
        }

        // GET: Utilizadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizador
                .FirstOrDefaultAsync(m => m.IdUtilizador == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // POST: Utilizadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizador = await _context.Utilizador.FindAsync(id);
            _context.Utilizador.Remove(utilizador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadorExists(int id)
        {
            return _context.Utilizador.Any(e => e.IdUtilizador == id);
        }



        //------------------------------Funções criadas por mim---------------------------------------------

        //Register----------------------------------------------------------------
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("IdUtilizador,UserName,Password,Email")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {

                Utilizador u = _context.Utilizador.SingleOrDefault(u => u.UserName == utilizador.UserName);
                Utilizador e = _context.Utilizador.SingleOrDefault(e => e.Email == utilizador.Email);

                if (u == null && e == null)
                {
                    utilizador.Categoria = 1;//vai ser sempre registo em utilizador

                    _context.Add(utilizador);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("Utilizador", utilizador.UserName);
                    HttpContext.Session.SetString("UtilizadorId", utilizador.IdUtilizador.ToString());
                    HttpContext.Session.SetInt32("Categoria", utilizador.Categoria);
                    return RedirectToAction("Index", "Home");
                }
                if (u != null)
                {
                    ModelState.AddModelError("ErroUserNameExistente", "Ja existe um utilizador com esse Username");

                }
                if (e != null)
                {
                    ModelState.AddModelError("ErroUserNameExistente", "Ja existe um utilizador com esse Email");

                }
            }
            return View();
        }

        //LogIN----------------------------------------------------------------------
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string UserName, string Password)
        {
            if (ModelState.IsValid)
            {
                Utilizador u = _context.Utilizador.SingleOrDefault(u => u.UserName == UserName && u.Password == Password);

                //se o username ou a passowd não forem exatamente iguais minusculas ou maiusculas vai dar erro
                if (!u.UserName.Equals(UserName) || !u.Password.Equals(Password))
                {
                    u = null;
                }

                if (u == null)
                {
                    ModelState.AddModelError("username", "Username ou password está incorreta !!");
                }
                else
                {
                        HttpContext.Session.SetString("Utilizador", UserName);
                        HttpContext.Session.SetString("UtilizadorId", u.IdUtilizador.ToString());

                    //verificar qual a categoria do utilizador e meter na cookie
                    //0-admin, 1-utilizador, -1 Admin Master

                    HttpContext.Session.SetInt32("Categoria", u.Categoria);
           

                    return RedirectToAction("Index", "Home");

                }
            }
            return View();
        }

        //Logout ---------------------------------------------------------------------------
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(".ProjetoSia.Session");
            return RedirectToAction("Index", "Home");
        }
        //Fim-Logiut---------------------------------------------

        //-----------------------------------gerir Utilizadores-------------------------------
        public async Task<IActionResult> GerirUtilizadores()
        {
            return View(await _context.Utilizador.ToListAsync());
           
        }

        //// POST: Utilizadors/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> GerirUtilizadores()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //_context.Add(utilizador);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}

        //-----------------------------------------------------------------------------------


        //--------------------DAR ADMIN-------------------------------------------------------
        public async Task<IActionResult> DarAdmin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizador.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            else
            {
                //se for admin master ou admin não faz nada
                //se for utilizador
                if(utilizador.Categoria == 1)
                {
                    utilizador.Categoria = 0;
                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();
                    
                }

            }
            //no final vai returnar a view que já estava, neste caso gerir utilizadores
            return RedirectToAction("GerirUtilizadores", "Utilizadors");
        }


        //--------------------RETIRAR ADMIN-------------------------------------------------------
        public async Task<IActionResult> RetirarAdmin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizador.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            else
            {
                //se for admin master não faz nada
                //se for admin pode retirar admin
                if (utilizador.Categoria == 0)
                {
                    utilizador.Categoria = 1;                 
                }
                _context.Update(utilizador);
                await _context.SaveChangesAsync();

            }
            //no final vai returnar a view que já estava, neste caso gerir utilizadores
            return RedirectToAction("GerirUtilizadores", "Utilizadors");
        }


        //------------------------------------------Perfil------------------------------
        public IActionResult Perfil(int? id)
        {
            Utilizador u = _context.Utilizador.SingleOrDefault(u => u.IdUtilizador == id);

            if (u == null)
            {
                return NotFound();
            }
            else
                return View(u);
        }

    }
}
