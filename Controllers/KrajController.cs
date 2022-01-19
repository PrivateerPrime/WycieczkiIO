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
    public class KrajController : Controller
    {
        private readonly MyDbContext _context;

        public KrajController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Kraj
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kraj.ToListAsync());
        }

        // GET: Kraj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kraj = await _context.Kraj
                .FirstOrDefaultAsync(m => m.KrajId == id);
            if (kraj == null)
            {
                return NotFound();
            }

            return View(kraj);
        }

        // GET: Kraj/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kraj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KrajId,NazwaKraju")] Kraj kraj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kraj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kraj);
        }

        // GET: Kraj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kraj = await _context.Kraj.FindAsync(id);
            if (kraj == null)
            {
                return NotFound();
            }
            return View(kraj);
        }

        // POST: Kraj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KrajId,NazwaKraju")] Kraj kraj)
        {
            if (id != kraj.KrajId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kraj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KrajExists(kraj.KrajId))
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
            return View(kraj);
        }

        // GET: Kraj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kraj = await _context.Kraj
                .FirstOrDefaultAsync(m => m.KrajId == id);
            if (kraj == null)
            {
                return NotFound();
            }

            return View(kraj);
        }

        // POST: Kraj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kraj = await _context.Kraj.FindAsync(id);
            _context.Kraj.Remove(kraj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KrajExists(int id)
        {
            return _context.Kraj.Any(e => e.KrajId == id);
        }
    }
}
