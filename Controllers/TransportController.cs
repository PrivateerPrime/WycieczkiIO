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
    public class TransportController : Controller
    {
        private readonly MyDbContext _context;

        public TransportController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Transport
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Transport.Include(t => t.AdresKoniec).Include(t => t.AdresPoczatek);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Transport/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport
                .Include(t => t.AdresKoniec)
                .Include(t => t.AdresPoczatek)
                .FirstOrDefaultAsync(m => m.TransportId == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // GET: Transport/Create
        public IActionResult Create()
        {
            
            ViewData["AdresKoniec"] = new SelectList(_context.Adres, "AdresId", "Ulica");
            ViewData["AdresPoczatek"] = new SelectList(_context.Adres, "AdresId", "Ulica");
            return View();
        }

        // POST: Transport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransportId,AdresPoczatekId,AdresKoniecId,RodzajTransportu")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresKoniecId"] = new SelectList(_context.Adres, "AdresId", "Ulica", transport.AdresKoniecId);
            ViewData["AdresPoczatekId"] = new SelectList(_context.Adres, "AdresId", "Ulica", transport.AdresPoczatekId);
            return View(transport);
        }

        // GET: Transport/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }
            ViewData["AdresKoniecId"] = new SelectList(_context.Adres, "AdresId", "Ulica", transport.AdresKoniecId);
            ViewData["AdresPoczatekId"] = new SelectList(_context.Adres, "AdresId", "Ulica", transport.AdresPoczatekId);
            return View(transport);
        }

        // POST: Transport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransportId,AdresPoczatekId,AdresKoniecId,RodzajTransportu")] Transport transport)
        {
            if (id != transport.TransportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportExists(transport.TransportId))
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
            ViewData["AdresKoniecId"] = new SelectList(_context.Adres, "AdresId", "Ulica", transport.AdresKoniecId);
            ViewData["AdresPoczatekId"] = new SelectList(_context.Adres, "AdresId", "Ulica", transport.AdresPoczatekId);
            return View(transport);
        }

        // GET: Transport/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transport
                .Include(t => t.AdresKoniec)
                .Include(t => t.AdresPoczatek)
                .FirstOrDefaultAsync(m => m.TransportId == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // POST: Transport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transport = await _context.Transport.FindAsync(id);
            _context.Transport.Remove(transport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportExists(int id)
        {
            return _context.Transport.Any(e => e.TransportId == id);
        }
    }
}
