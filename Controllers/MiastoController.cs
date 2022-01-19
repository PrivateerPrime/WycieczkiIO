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
    public class MiastoController : Controller
    {
        private readonly MyDbContext _context;

        public MiastoController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Miasto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Miasto.ToListAsync());
        }

        // GET: Miasto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miasto = await _context.Miasto
                .FirstOrDefaultAsync(m => m.MiastoId == id);
            if (miasto == null)
            {
                return NotFound();
            }

            return View(miasto);
        }

        // GET: Miasto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miasto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MiastoId,NazwaMiasta")] Miasto miasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miasto);
        }

        // GET: Miasto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miasto = await _context.Miasto.FindAsync(id);
            if (miasto == null)
            {
                return NotFound();
            }
            return View(miasto);
        }

        // POST: Miasto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MiastoId,NazwaMiasta")] Miasto miasto)
        {
            if (id != miasto.MiastoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiastoExists(miasto.MiastoId))
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
            return View(miasto);
        }

        // GET: Miasto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miasto = await _context.Miasto
                .FirstOrDefaultAsync(m => m.MiastoId == id);
            if (miasto == null)
            {
                return NotFound();
            }

            return View(miasto);
        }

        // POST: Miasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miasto = await _context.Miasto.FindAsync(id);
            _context.Miasto.Remove(miasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiastoExists(int id)
        {
            return _context.Miasto.Any(e => e.MiastoId == id);
        }
    }
}
