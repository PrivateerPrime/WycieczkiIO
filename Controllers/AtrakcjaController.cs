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
    public class AtrakcjaController : Controller
    {
        private readonly MyDbContext _context;

        public AtrakcjaController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Atrakcja
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Atrakcja.Include(a => a.Przewodnik);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Atrakcja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atrakcja = await _context.Atrakcja
                .Include(a => a.Przewodnik)
                .FirstOrDefaultAsync(m => m.AtrakcjaId == id);
            if (atrakcja == null)
            {
                return NotFound();
            }

            return View(atrakcja);
        }

        // GET: Atrakcja/Create
        public IActionResult Create()
        {
            ViewData["PrzewodnikId"] = new SelectList(_context.Przewodnik, "PrzewodnikId", "Imie");
            return View();
        }

        // POST: Atrakcja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AtrakcjaId,Nazwa,PrzewodnikId")] Atrakcja atrakcja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atrakcja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrzewodnikId"] = new SelectList(_context.Przewodnik, "PrzewodnikId", "Imie", atrakcja.PrzewodnikId);
            return View(atrakcja);
        }

        // GET: Atrakcja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atrakcja = await _context.Atrakcja.FindAsync(id);
            if (atrakcja == null)
            {
                return NotFound();
            }
            ViewData["PrzewodnikId"] = new SelectList(_context.Przewodnik, "PrzewodnikId", "Imie", atrakcja.PrzewodnikId);
            return View(atrakcja);
        }

        // POST: Atrakcja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AtrakcjaId,Nazwa,PrzewodnikId")] Atrakcja atrakcja)
        {
            if (id != atrakcja.AtrakcjaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atrakcja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtrakcjaExists(atrakcja.AtrakcjaId))
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
            ViewData["PrzewodnikId"] = new SelectList(_context.Przewodnik, "PrzewodnikId", "Imie", atrakcja.PrzewodnikId);
            return View(atrakcja);
        }

        // GET: Atrakcja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atrakcja = await _context.Atrakcja
                .Include(a => a.Przewodnik)
                .FirstOrDefaultAsync(m => m.AtrakcjaId == id);
            if (atrakcja == null)
            {
                return NotFound();
            }

            return View(atrakcja);
        }

        // POST: Atrakcja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atrakcja = await _context.Atrakcja.FindAsync(id);
            _context.Atrakcja.Remove(atrakcja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtrakcjaExists(int id)
        {
            return _context.Atrakcja.Any(e => e.AtrakcjaId == id);
        }
    }
}
