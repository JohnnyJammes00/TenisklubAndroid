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
    public class IgralecController : Controller
    {
        private readonly TenisKlubContext _context;

        public IgralecController(TenisKlubContext context)
        {
            _context = context;
        }

        // GET: Igralec
        public async Task<IActionResult> Index()
        {
            return View(await _context.Igralci.ToListAsync());
        }

        // GET: Igralec/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igralec = await _context.Igralci
                .FirstOrDefaultAsync(m => m.ID == id);
            if (igralec == null)
            {
                return NotFound();
            }

            return View(igralec);
        }

        // GET: Igralec/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Igralec/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ime,Priimek")] Igralec igralec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(igralec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(igralec);
        }

        // GET: Igralec/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igralec = await _context.Igralci.FindAsync(id);
            if (igralec == null)
            {
                return NotFound();
            }
            return View(igralec);
        }

        // POST: Igralec/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ime,Priimek")] Igralec igralec)
        {
            if (id != igralec.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(igralec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgralecExists(igralec.ID))
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
            return View(igralec);
        }

        // GET: Igralec/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igralec = await _context.Igralci
                .FirstOrDefaultAsync(m => m.ID == id);
            if (igralec == null)
            {
                return NotFound();
            }

            return View(igralec);
        }

        // POST: Igralec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var igralec = await _context.Igralci.FindAsync(id);
            var tekme = _context.Tekme.Where(t => t.Igralec1ID == id || t.Igralec2ID == id);
            _context.Tekme.RemoveRange(tekme);
            _context.Igralci.Remove(igralec);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IgralecExists(int id)
        {
            return _context.Igralci.Any(e => e.ID == id);
        }
    }
}
