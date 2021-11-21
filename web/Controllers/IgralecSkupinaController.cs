using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace web.Controllers
{
    [Authorize]
    public class IgralecSkupinaController : Controller
    {
        private readonly TenisKlubContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public IgralecSkupinaController(TenisKlubContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: IgralecSkupina
        public async Task<IActionResult> Index()
        {
            var tenisKlubContext = _context.IgralecSkupine.Include(i => i.Igralec).Include(i => i.Skupina);
            return View(await tenisKlubContext.ToListAsync());
        }

        // GET: IgralecSkupina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igralecSkupina = await _context.IgralecSkupine
                .Include(i => i.Igralec)
                .Include(i => i.Skupina)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (igralecSkupina == null)
            {
                return NotFound();
            }

            return View(igralecSkupina);
        }

        // GET: IgralecSkupina/Create
        public IActionResult Create()
        {
            var Igralci = _context.Igralci.Select(i => new {
                ID = i.ID,
                Ime = i.Ime + " " + i.Priimek
            });
            ViewData["IgralecID"] = new SelectList(Igralci, "ID", "Ime");
            ViewData["SkupinaID"] = new SelectList(_context.Skupine, "ID", "ImeSkupine");
            return View();
        }

        // POST: IgralecSkupina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IgralecID,SkupinaID")] IgralecSkupina igralecSkupina)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                igralecSkupina.DateJoined = DateTime.Now;
                igralecSkupina.DateEdited = DateTime.Now;
                _context.Add(igralecSkupina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IgralecID"] = new SelectList(_context.Igralci, "ID", "ID", igralecSkupina.IgralecID);
            ViewData["SkupinaID"] = new SelectList(_context.Skupine, "ID", "ID", igralecSkupina.SkupinaID);
            return View(igralecSkupina);
        }

        // GET: IgralecSkupina/Edit/5

        [Authorize(Roles = "Administrator, Manager, Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igralecSkupina = await _context.IgralecSkupine.FindAsync(id);
            if (igralecSkupina == null)
            {
                return NotFound();
            }
            ViewData["IgralecID"] = new SelectList(_context.Igralci, "ID", "ID", igralecSkupina.IgralecID);
            ViewData["SkupinaID"] = new SelectList(_context.Skupine, "ID", "ID", igralecSkupina.SkupinaID);
            return View(igralecSkupina);
        }

        // POST: IgralecSkupina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IgralecID,SkupinaID")] IgralecSkupina igralecSkupina)
        {
            if (id != igralecSkupina.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(igralecSkupina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgralecSkupinaExists(igralecSkupina.ID))
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
            ViewData["IgralecID"] = new SelectList(_context.Igralci, "ID", "ID", igralecSkupina.IgralecID);
            ViewData["SkupinaID"] = new SelectList(_context.Skupine, "ID", "ID", igralecSkupina.SkupinaID);
            return View(igralecSkupina);
        }

        // GET: IgralecSkupina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igralecSkupina = await _context.IgralecSkupine
                .Include(i => i.Igralec)
                .Include(i => i.Skupina)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (igralecSkupina == null)
            {
                return NotFound();
            }

            return View(igralecSkupina);
        }

        // POST: IgralecSkupina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var igralecSkupina = await _context.IgralecSkupine.FindAsync(id);
            _context.IgralecSkupine.Remove(igralecSkupina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IgralecSkupinaExists(int id)
        {
            return _context.IgralecSkupine.Any(e => e.ID == id);
        }
    }
}
