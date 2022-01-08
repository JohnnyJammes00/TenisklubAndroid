using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class TekmaController : Controller
    {
        private readonly TenisKlubContext _context;

        public TekmaController(TenisKlubContext context)
        {
            _context = context;
        }

        // GET: Tekma
        public async Task<IActionResult> Index()
        {
            var tenisKlubContext = _context.Tekme.Include(t => t.Igralec1).Include(t => t.Igralec2).Include(t => t.Igrisce);
            return View(await tenisKlubContext.ToListAsync());
        }

        // GET: Tekma/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tekma = await _context.Tekme
                .Include(t => t.Igralec1)
                .Include(t => t.Igralec2)
                .Include(t => t.Igrisce)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tekma == null)
            {
                return NotFound();
            }

            return View(tekma);
        }

        // GET: Tekma/Create
        public IActionResult Create()
        {
            var Igralci = _context.Igralci.Select(i => new {
                ID = i.ID,
                Ime = i.Ime + " " + i.Priimek
            });
            ViewData["Igralec1ID"] = new SelectList(Igralci, "ID", "Ime");
            ViewData["Igralec2ID"] = new SelectList(Igralci, "ID", "Ime");
            ViewData["IgrisceID"] = new SelectList(_context.Igrisca, "ID", "Naziv");
            return View();
        }

        // POST: Tekma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Igralec1ID,Igralec2ID,Score1,Score2,Winner,Date,IgrisceID")] Tekma tekma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tekma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Igralec1ID"] = new SelectList(_context.Igralci, "ID", "ID", tekma.Igralec1ID);
            ViewData["Igralec2ID"] = new SelectList(_context.Igralci, "ID", "ID", tekma.Igralec2ID);
            ViewData["IgrisceID"] = new SelectList(_context.Igrisca, "ID", "ID", tekma.IgrisceID);
            return View(tekma);
        }

        // GET: Tekma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tekma = await _context.Tekme.FindAsync(id);
            if (tekma == null)
            {
                return NotFound();
            }
            ViewData["Igralec1ID"] = new SelectList(_context.Igralci, "ID", "ID", tekma.Igralec1ID);
            ViewData["Igralec2ID"] = new SelectList(_context.Igralci, "ID", "ID", tekma.Igralec2ID);
            ViewData["IgrisceID"] = new SelectList(_context.Igrisca, "ID", "ID", tekma.IgrisceID);
            return View(tekma);
        }

        // POST: Tekma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Igralec1ID,Igralec2ID,Score1,Score2,Winner,Date,IgrisceID")] Tekma tekma)
        {
            if (id != tekma.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tekma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TekmaExists(tekma.ID))
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
            ViewData["Igralec1ID"] = new SelectList(_context.Igralci, "ID", "ID", tekma.Igralec1ID);
            ViewData["Igralec2ID"] = new SelectList(_context.Igralci, "ID", "ID", tekma.Igralec2ID);
            ViewData["IgrisceID"] = new SelectList(_context.Igrisca, "ID", "ID", tekma.IgrisceID);
            return View(tekma);
        }

        // GET: Tekma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tekma = await _context.Tekme
                .Include(t => t.Igralec1)
                .Include(t => t.Igralec2)
                .Include(t => t.Igrisce)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tekma == null)
            {
                return NotFound();
            }

            return View(tekma);
        }

        // POST: Tekma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tekma = await _context.Tekme.FindAsync(id);
            _context.Tekme.Remove(tekma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TekmaExists(int id)
        {
            return _context.Tekme.Any(e => e.ID == id);
        }
    }
}
