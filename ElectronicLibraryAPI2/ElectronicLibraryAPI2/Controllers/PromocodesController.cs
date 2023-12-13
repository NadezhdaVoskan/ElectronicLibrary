using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicLibraryAPI2.Models;

namespace ElectronicLibraryAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocodesController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public PromocodesController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Promocodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promocode>>> GetPromocodes()
        {
          if (_context.Promocodes == null)
          {
              return NotFound();
          }
            return await _context.Promocodes.ToListAsync();
        }

        // GET: api/Promocodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Promocode>> GetPromocode(int id)
        {
          if (_context.Promocodes == null)
          {
              return NotFound();
          }
            var promocode = await _context.Promocodes.FindAsync(id);

            if (promocode == null)
            {
                return NotFound();
            }

            return promocode;
        }

        // PUT: api/Promocodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromocode(int id, Promocode promocode)
        {
            if (id != promocode.IdPromocode)
            {
                return BadRequest();
            }

            _context.Entry(promocode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromocodeExists(id))
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

        // POST: api/Promocodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Promocode>> PostPromocode(Promocode promocode)
        {
          if (_context.Promocodes == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.Promocodes'  is null.");
          }
            _context.Promocodes.Add(promocode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromocode", new { id = promocode.IdPromocode }, promocode);
        }

        // DELETE: api/Promocodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromocode(int id)
        {
            if (_context.Promocodes == null)
            {
                return NotFound();
            }
            var promocode = await _context.Promocodes.FindAsync(id);
            if (promocode == null)
            {
                return NotFound();
            }

            _context.Promocodes.Remove(promocode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PromocodeExists(int id)
        {
            return (_context.Promocodes?.Any(e => e.IdPromocode == id)).GetValueOrDefault();
        }
    }
}
