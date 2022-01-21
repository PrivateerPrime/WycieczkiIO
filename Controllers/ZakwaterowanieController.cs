using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WycieczkiIO.Data;
using WycieczkiIO.Models;

namespace WycieczkiIO.Controllers
{
    public class ZakwaterowanieController : Controller
    {
        private readonly MyDbContext _context;

        public ZakwaterowanieController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Zakwaterowanie
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Zakwaterowanie.Include(z => z.Adres);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Zakwaterowanie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zakwaterowanie = await _context.Zakwaterowanie
                .Include(z => z.Adres)
                .FirstOrDefaultAsync(m => m.ZakwaterowanieId == id);
            if (zakwaterowanie == null)
            {
                return NotFound();
            }

            var adres = await _context.Adres.FirstOrDefaultAsync(elem => elem.AdresId == zakwaterowanie.AdresId);
            var miasto = await _context.Miasto.FirstOrDefaultAsync(elem => elem.MiastoId == adres.MiastoId);
            var kraj = await _context.Kraj.FirstOrDefaultAsync(elem => elem.KrajId == adres.KrajId);
            ViewData["Ulica"] = adres.Ulica;
            ViewData["Numer"] = adres.Numer;
            ViewData["KodPocztowy"] = adres.KodPocztowy;
            ViewData["NazwaMiasta"] = miasto.NazwaMiasta;
            ViewData["NazwaKraju"] = kraj.NazwaKraju;
            return View(zakwaterowanie);
        }

        // GET: Zakwaterowanie/Create
        public IActionResult Create()
        {
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "Ulica");
            return View();
        }

        // POST: Zakwaterowanie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZakwaterowanieId,AdresId,Nazwa,Typ")] Zakwaterowanie zakwaterowanie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zakwaterowanie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "Ulica", zakwaterowanie.AdresId);
            return View(zakwaterowanie);
        }

        // GET: Zakwaterowanie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zakwaterowanie = await _context.Zakwaterowanie.FindAsync(id);
            if (zakwaterowanie == null)
            {
                return NotFound();
            }
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "Ulica", zakwaterowanie.AdresId);
            return View(zakwaterowanie);
        }

        // POST: Zakwaterowanie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZakwaterowanieId,AdresId,Nazwa,Typ")] Zakwaterowanie zakwaterowanie)
        {
            if (id != zakwaterowanie.ZakwaterowanieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zakwaterowanie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZakwaterowanieExists(zakwaterowanie.ZakwaterowanieId))
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
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "Ulica", zakwaterowanie.AdresId);
            return View(zakwaterowanie);
        }

        // GET: Zakwaterowanie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zakwaterowanie = await _context.Zakwaterowanie
                .Include(z => z.Adres)
                .FirstOrDefaultAsync(m => m.ZakwaterowanieId == id);
            if (zakwaterowanie == null)
            {
                return NotFound();
            }
            var adres = await _context.Adres.FirstOrDefaultAsync(elem => elem.AdresId == zakwaterowanie.AdresId);
            var miasto = await _context.Miasto.FirstOrDefaultAsync(elem => elem.MiastoId == adres.MiastoId);
            var kraj = await _context.Kraj.FirstOrDefaultAsync(elem => elem.KrajId == adres.KrajId);
            ViewData["Ulica"] = adres.Ulica;
            ViewData["Numer"] = adres.Numer;
            ViewData["KodPocztowy"] = adres.KodPocztowy;
            ViewData["NazwaMiasta"] = miasto.NazwaMiasta;
            ViewData["NazwaKraju"] = kraj.NazwaKraju;
            return View(zakwaterowanie);
        }

        // POST: Zakwaterowanie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zakwaterowanie = await _context.Zakwaterowanie.FindAsync(id);
            _context.Zakwaterowanie.Remove(zakwaterowanie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZakwaterowanieExists(int id)
        {
            return _context.Zakwaterowanie.Any(e => e.ZakwaterowanieId == id);
        }
    }
}
