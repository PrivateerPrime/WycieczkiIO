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
    public class PrzewodnikController : Controller
    {
        private readonly MyDbContext _context;

        public PrzewodnikController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Przewodnik
        public async Task<IActionResult> Index()
        {
            return View(await _context.Przewodnik.ToListAsync());
        }

        // GET: Przewodnik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przewodnik = await _context.Przewodnik
                .FirstOrDefaultAsync(m => m.PrzewodnikId == id);
            if (przewodnik == null)
            {
                return NotFound();
            }

            return View(przewodnik);
        }

        // GET: Przewodnik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Przewodnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrzewodnikId,Imie,Nazwisko,Telefon")] Przewodnik przewodnik)
        {
            przewodnik.Atrakcja = new List<Atrakcja>();
            if (ModelState.IsValid)
            {
                _context.Add(przewodnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(przewodnik);
        }

        // GET: Przewodnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przewodnik = await _context.Przewodnik.FindAsync(id);
            if (przewodnik == null)
            {
                return NotFound();
            }
            return View(przewodnik);
        }

        // POST: Przewodnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrzewodnikId,Imie,Nazwisko,Telefon")] Przewodnik przewodnik)
        {
            if (id != przewodnik.PrzewodnikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przewodnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzewodnikExists(przewodnik.PrzewodnikId))
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
            return View(przewodnik);
        }

        // GET: Przewodnik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przewodnik = await _context.Przewodnik
                .FirstOrDefaultAsync(m => m.PrzewodnikId == id);
            if (przewodnik == null)
            {
                return NotFound();
            }

            return View(przewodnik);
        }

        // POST: Przewodnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przewodnik = await _context.Przewodnik.FindAsync(id);
            _context.Przewodnik.Remove(przewodnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzewodnikExists(int id)
        {
            return _context.Przewodnik.Any(e => e.PrzewodnikId == id);
        }
    }
}
