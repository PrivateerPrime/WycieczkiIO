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
    public class PlatnoscController : Controller
    {
        private readonly MyDbContext _context;

        public PlatnoscController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Platnosc
        public async Task<IActionResult> Index()
        {
            return View(await _context.Platnosc.ToListAsync());
        }

        // GET: Platnosc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platnosc = await _context.Platnosc
                .FirstOrDefaultAsync(m => m.PlatnoscId == id);
            if (platnosc == null)
            {
                return NotFound();
            }

            return View(platnosc);
        }

        // GET: Platnosc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platnosc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlatnoscId,Kwota,Rabat,Status")] Platnosc platnosc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platnosc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(platnosc);
        }

        // GET: Platnosc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platnosc = await _context.Platnosc.FindAsync(id);
            if (platnosc == null)
            {
                return NotFound();
            }
            return View(platnosc);
        }

        // POST: Platnosc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlatnoscId,Kwota,Rabat,Status")] Platnosc platnosc)
        {
            if (id != platnosc.PlatnoscId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platnosc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatnoscExists(platnosc.PlatnoscId))
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
            return View(platnosc);
        }

        // GET: Platnosc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platnosc = await _context.Platnosc
                .FirstOrDefaultAsync(m => m.PlatnoscId == id);
            if (platnosc == null)
            {
                return NotFound();
            }

            return View(platnosc);
        }

        // POST: Platnosc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platnosc = await _context.Platnosc.FindAsync(id);
            _context.Platnosc.Remove(platnosc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlatnoscExists(int id)
        {
            return _context.Platnosc.Any(e => e.PlatnoscId == id);
        }
    }
}
