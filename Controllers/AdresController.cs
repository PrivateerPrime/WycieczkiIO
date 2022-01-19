using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WycieczkiIO.Data;
using WycieczkiIO.Models;

namespace WycieczkiIO.Controllers
{
    public class AdresController : Controller
    {
        private readonly MyDbContext _context;

        public AdresController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Adres
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Adres.Include(a => a.Kraj).Include(a => a.Miasto);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Adres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adres = await _context.Adres
                .Include(a => a.Kraj)
                .Include(a => a.Miasto)
                .FirstOrDefaultAsync(m => m.AdresId == id);
            if (adres == null)
            {
                return NotFound();
            }

            return View(adres);
        }

        // GET: Adres/Create
        public IActionResult Create()
        {
            ViewData["KrajId"] = new SelectList(_context.Kraj, "KrajId", "NazwaKraju");
            ViewData["MiastoId"] = new SelectList(_context.Miasto, "MiastoId", "NazwaMiasta");
            return View();
        }

        // POST: Adres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdresId,Ulica,Numer,KodPocztowy,MiastoId,KrajId")] Adres adres)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adres);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KrajId"] = new SelectList(_context.Kraj, "KrajId", "NazwaKraju", adres.KrajId);
            ViewData["MiastoId"] = new SelectList(_context.Miasto, "MiastoId", "NazwaMiasta", adres.MiastoId);
            return View(adres);
        }

        // GET: Adres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adres = await _context.Adres.FindAsync(id);
            if (adres == null)
            {
                return NotFound();
            }
            ViewData["KrajId"] = new SelectList(_context.Kraj, "KrajId", "NazwaKraju", adres.KrajId);
            ViewData["MiastoId"] = new SelectList(_context.Miasto, "MiastoId", "NazwaMiasta", adres.MiastoId);
            return View(adres);
        }

        // POST: Adres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdresId,Ulica,Numer,KodPocztowy,MiastoId,KrajId")] Adres adres)
        {
            if (id != adres.AdresId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adres);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdresExists(adres.AdresId))
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
            ViewData["KrajId"] = new SelectList(_context.Kraj, "KrajId", "NazwaKraju", adres.KrajId);
            ViewData["MiastoId"] = new SelectList(_context.Miasto, "MiastoId", "NazwaMiasta", adres.MiastoId);
            return View(adres);
        }

        // GET: Adres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adres = await _context.Adres
                .Include(a => a.Kraj)
                .Include(a => a.Miasto)
                .FirstOrDefaultAsync(m => m.AdresId == id);
            if (adres == null)
            {
                return NotFound();
            }

            return View(adres);
        }

        // POST: Adres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adres = await _context.Adres.FindAsync(id);
            _context.Adres.Remove(adres);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdresExists(int id)
        {
            return _context.Adres.Any(e => e.AdresId == id);
        }
    }
}
