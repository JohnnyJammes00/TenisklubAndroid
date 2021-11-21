using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class RezervacijaController : Controller
    {
        private readonly TenisKlubContext _context;

        public RezervacijaController(TenisKlubContext context)
        {
            _context = context;
        }

        // GET: Rezervacija
        // TODO only show reservations for current user or Admin
        // [Authorize(Roles = "Admin")] //... Comes from Identity not implemented yet.
        public async Task<IActionResult> Index()
        {
            var tenisKlubContext = _context.Rezervacije.Include(r => r.Igralec).Include(r => r.Igrisce);
            return View(await tenisKlubContext.ToListAsync());
        }

        // GET: Rezervacija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije
                .Include(r => r.Igralec)
                .Include(r => r.Igrisce)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // GET: Rezervacija/Create

         [Authorize]
        public IActionResult Create()
       
        {

            var Igralci = _context.Igralci.Select(i => new {
                ID = i.ID,
                Ime = i.Ime + " " + i.Priimek
            });
            ViewData["IgralecID"] = new SelectList(Igralci, "ID", "Ime");
            ViewData["IgrisceID"] = new SelectList(_context.Igrisca, "ID", "Naziv");
            return View();
        }

        // POST: Rezervacija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DatumRezervacije,IgralecID,IgrisceID,Tranjanje")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IgralecID"] = new SelectList(_context.Igralci, "ID", "ID", rezervacija.IgralecID);
            ViewData["IgrisceID"] = new SelectList(_context.Igrisca, "ID", "ID", rezervacija.IgrisceID);
            return View(rezervacija);
        }

        // GET: Rezervacija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            ViewData["IgralecID"] = new SelectList(_context.Igralci, "ID", "ID", rezervacija.IgralecID);
            ViewData["IgrisceID"] = new SelectList(_context.Igrisca, "ID", "ID", rezervacija.IgrisceID);
            return View(rezervacija);
        }



        [Authorize]
        // POST: Rezervacija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DatumRezervacije,IgralecID,IgrisceID,Tranjanje")] Rezervacija rezervacija)
        {
            if (id != rezervacija.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.ID))
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
            ViewData["IgralecID"] = new SelectList(_context.Igralci, "ID", "ID", rezervacija.IgralecID);
            ViewData["IgrisceID"] = new SelectList(_context.Igrisca, "ID", "ID", rezervacija.IgrisceID);
            return View(rezervacija);
        }


        [Authorize]
        // GET: Rezervacija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.Rezervacije
                .Include(r => r.Igralec)
                .Include(r => r.Igrisce)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // POST: Rezervacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.Rezervacije.FindAsync(id);
            _context.Rezervacije.Remove(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacije.Any(e => e.ID == id);
        }
    }
}
