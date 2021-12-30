using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/v1/Igralec")]
    [ApiController]
    public class IgralecApiController : ControllerBase
    {
        private readonly TenisKubContext _context;

        public IgralecApiController(TenisKubContext context)
        {
            _context = context;
        }

        // GET: api/IgralecApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Igralec>>> GetIgralec()
        {
            return await _context.Igralec.ToListAsync();
        }

        // GET: api/IgralecApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Igralec>> GetIgralec(int id)
        {
            var igralec = await _context.Igralec.FindAsync(id);

            if (igralec == null)
            {
                return NotFound();
            }

            return igralec;
        }

        // PUT: api/IgralecApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIgralec(int id, Igralec igralec)
        {
            if (id != igralec.ID)
            {
                return BadRequest();
            }

            _context.Entry(igralec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IgralecExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/IgralecApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Igralec>> PostIgralec(Igralec igralec)
        {
            _context.Igralec.Add(igralec);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIgralec", new { id = igralec.ID }, igralec);
        }

        // DELETE: api/IgralecApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIgralec(int id)
        {
            var igralec = await _context.Igralec.FindAsync(id);
            if (igralec == null)
            {
                return NotFound();
            }

            _context.Igralec.Remove(igralec);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IgralecExists(int id)
        {
            return _context.Igralec.Any(e => e.ID == id);
        }
    }
}
