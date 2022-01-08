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
    public class SkupinaController : Controller
    {
        private readonly TenisKlubContext _context;

        public SkupinaController(TenisKlubContext context)
        {
            _context = context;
        }

        // GET: Skupina
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skupine.ToListAsync());
        }

        // GET: Skupina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skupina = await _context.Skupine
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skupina == null)
            {
                return NotFound();
            }

            return View(skupina);
        }

        // GET: Skupina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skupina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ImeSkupine")] Skupina skupina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skupina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skupina);
        }

        // GET: Skupina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skupina = await _context.Skupine.FindAsync(id);
            if (skupina == null)
            {
                return NotFound();
            }
            return View(skupina);
        }

        // POST: Skupina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ImeSkupine")] Skupina skupina)
        {
            if (id != skupina.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skupina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkupinaExists(skupina.ID))
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
            return View(skupina);
        }

        // GET: Skupina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skupina = await _context.Skupine
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skupina == null)
            {
                return NotFound();
            }

            return View(skupina);
        }

        // POST: Skupina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skupina = await _context.Skupine.FindAsync(id);
            _context.Skupine.Remove(skupina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkupinaExists(int id)
        {
            return _context.Skupine.Any(e => e.ID == id);
        }
    }
}
