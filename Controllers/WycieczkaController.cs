using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WycieczkiIO.Data;
using WycieczkiIO.Models;

namespace WycieczkiIO.Controllers
{
    public class WycieczkaController : Controller
    {
        private readonly MyDbContext _context;

        public WycieczkaController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Wycieczka
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Wycieczka.Include(w => w.Platnosc).Include(w => w.Zakwaterowanie);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Wycieczka/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wycieczka = await _context.Wycieczka
                .Include(w => w.Platnosc)
                .Include(w => w.Zakwaterowanie)
                .FirstOrDefaultAsync(m => m.WycieczkaId == id);
            if (wycieczka == null)
            {
                return NotFound();
            }

            var platnosc = await _context.Platnosc.FirstOrDefaultAsync(elem => elem.PlatnoscId == wycieczka.PlatnoscId);
            var zakwaterowanie = await _context.Zakwaterowanie.FirstOrDefaultAsync(elem =>
                elem.ZakwaterowanieId == wycieczka.ZakwaterowanieId);
            ViewData["Kwota"] = platnosc.Kwota;
            ViewData["Rabat"] = platnosc.Rabat;
            ViewData["StatusPlatnosci"] = platnosc.Status;
            ViewData["Uczestnicy"] =
                _context.Uczestnik.Where(elem => elem.WycieczkaId == wycieczka.WycieczkaId).ToList();
            if (zakwaterowanie is not null)
                ViewData["ZakwaterowanieNazwa"] = zakwaterowanie.Nazwa;
            else
                ViewData["ZakwaterowanieNazwa"] = "";
            return View(wycieczka);
        }

        // GET: Wycieczka/Create
        public IActionResult Create()
        {
            // ViewData["PlatnoscId"] = new SelectList(_context.Platnosc, "PlatnoscId", "PlatnoscId");
            // ViewData["ZakwaterowanieId"] = new SelectList(_context.Zakwaterowanie, "ZakwaterowanieId", "Nazwa");
            return View();
        }

        // POST: Wycieczka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WycieczkaId,DataRozpoczecia,DataZakonczenia,MiejsceDocelowe")] Wycieczka wycieczka)
        {
            if (ModelState.IsValid)
            {
                Platnosc platnosc = new Platnosc
                    {Kwota = 0, Rabat = 0, Status = Status.Niezaplacona};
                _context.Add(platnosc);
                await _context.SaveChangesAsync();
                wycieczka.Platnosc = platnosc;
                // wycieczka.PlatnoscId = idToPlatnosc;
                
                _context.Add(wycieczka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["ZakwaterowanieId"] = new SelectList(_context.Zakwaterowanie, "ZakwaterowanieId", "Nazwa", wycieczka.ZakwaterowanieId);
            return View(wycieczka);
        }

        // GET: Wycieczka/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wycieczka = await _context.Wycieczka.FindAsync(id);
            if (wycieczka == null)
            {
                return NotFound();
            }
            // ViewData["PlatnoscId"] = new SelectList(_context.Platnosc, "PlatnoscId", "PlatnoscId", wycieczka.PlatnoscId);
            // ViewData["ZakwaterowanieId"] = new SelectList(_context.Zakwaterowanie, "ZakwaterowanieId", "Nazwa", wycieczka.ZakwaterowanieId);
            return View(wycieczka);
        }

        // POST: Wycieczka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WycieczkaId,DataRozpoczecia,DataZakonczenia,MiejsceDocelowe")] Wycieczka wycieczka)
        {
            if (id != wycieczka.WycieczkaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wycieczka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WycieczkaExists(wycieczka.WycieczkaId))
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
            // ViewData["PlatnoscId"] = new SelectList(_context.Platnosc, "PlatnoscId", "PlatnoscId", wycieczka.PlatnoscId);
            // ViewData["ZakwaterowanieId"] = new SelectList(_context.Zakwaterowanie, "ZakwaterowanieId", "Nazwa", wycieczka.ZakwaterowanieId);
            return View(wycieczka);
        }

        // GET: Wycieczka/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wycieczka = await _context.Wycieczka
                .Include(w => w.Platnosc)
                .Include(w => w.Zakwaterowanie)
                .FirstOrDefaultAsync(m => m.WycieczkaId == id);
            if (wycieczka == null)
            {
                return NotFound();
            }

            return View(wycieczka);
        }

        // POST: Wycieczka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wycieczka = await _context.Wycieczka.FindAsync(id);
            _context.Wycieczka.Remove(wycieczka);
            var platnoscDoUsunecia = await _context.Platnosc.FindAsync(wycieczka.PlatnoscId);
            _context.Platnosc.Remove(platnoscDoUsunecia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddPerson(int? id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerson(int id, [Bind("Imie","Nazwisko","Pesel","Telefon","Email")] Uczestnik uczestnik)
        {
            if (ModelState.IsValid)
            {
                uczestnik.WycieczkaId = id;
                _context.Add(uczestnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(uczestnik);
        }

        public IActionResult AddAccommodation(int? id)
        {
            ViewData["ZakwaterowanieId"] = new SelectList(_context.Zakwaterowanie, "ZakwaterowanieId", "Nazwa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccommodation(int id, int ZakwaterowanieId)
        {
            if (ModelState.IsValid)
            {
                var wycieczka = await _context.Wycieczka.FindAsync(id);
                wycieczka.ZakwaterowanieId = ZakwaterowanieId;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult DeletePerson(int? id)
        {
            return View(_context.Uczestnik.Where(elem => elem.WycieczkaId == id).ToList());
        }
        
        public async Task<IActionResult> DeletePersonConfirm(int? id)
        {
            var uczestnik = await _context.Uczestnik.FirstOrDefaultAsync(elem => elem.UczestnikId == id);
            return View(uczestnik);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePersonConfirm(int id)
        {
            if (ModelState.IsValid)
            {
                var uczestnik = await _context.Uczestnik.FindAsync(id);
                _context.Uczestnik.Remove(uczestnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DeletePerson), new {id = uczestnik.WycieczkaId});
            }

            return View();
        }

        public async Task<IActionResult> DeleteAccommodation(int? id)
        {
            return View(await _context.Wycieczka.FirstOrDefaultAsync(elem => elem.WycieczkaId == id));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccommodation(int id)
        {
            var wycieczka = await _context.Wycieczka.FirstOrDefaultAsync(elem => elem.WycieczkaId == id);
            wycieczka.ZakwaterowanieId = null;
            _context.Wycieczka.Update(wycieczka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool WycieczkaExists(int id)
        {
            return _context.Wycieczka.Any(e => e.WycieczkaId == id);
        }


        
    }
}
