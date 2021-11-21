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
    public class IgrisceController : Controller
    {
        private readonly TenisKlubContext _context;

        public IgrisceController(TenisKlubContext context)
        {
            _context = context;
        }

        // GET: Igrisce
        public async Task<IActionResult> Index()
        {
            return View(await _context.Igrisca.ToListAsync());
        }

        // GET: Igrisce/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrisce = await _context.Igrisca
                .FirstOrDefaultAsync(m => m.ID == id);
            if (igrisce == null)
            {
                return NotFound();
            }

            return View(igrisce);
        }

        // GET: Igrisce/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Igrisce/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,Cena")] Igrisce igrisce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(igrisce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(igrisce);
        }

        // GET: Igrisce/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrisce = await _context.Igrisca.FindAsync(id);
            if (igrisce == null)
            {
                return NotFound();
            }
            return View(igrisce);
        }

        // POST: Igrisce/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naziv,Cena")] Igrisce igrisce)
        {
            if (id != igrisce.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(igrisce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgrisceExists(igrisce.ID))
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
            return View(igrisce);
        }

        // GET: Igrisce/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrisce = await _context.Igrisca
                .FirstOrDefaultAsync(m => m.ID == id);
            if (igrisce == null)
            {
                return NotFound();
            }

            return View(igrisce);
        }

        // POST: Igrisce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var igrisce = await _context.Igrisca.FindAsync(id);
            _context.Igrisca.Remove(igrisce);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IgrisceExists(int id)
        {
            return _context.Igrisca.Any(e => e.ID == id);
        }
    }
}
