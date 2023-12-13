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
    public class RiderTicketsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public RiderTicketsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/RiderTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiderTicket>>> GetRiderTickets()
        {
            try
            {
                var ridertickets = await _context.RiderTickets
                    .Include(p => p.User)
                    .ToListAsync();

                if (ridertickets == null)
                {
                    return NotFound();
                }

                return ridertickets;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET: api/RiderTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RiderTicket>> GetRiderTicket(int? id)
        {
          if (_context.RiderTickets == null)
          {
              return NotFound();
          }
            var riderTicket = await _context.RiderTickets.FindAsync(id);

            if (riderTicket == null)
            {
                return NotFound();
            }

            return riderTicket;
        }

        // PUT: api/RiderTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRiderTicket(int? id, RiderTicket riderTicket)
        {
            if (id != riderTicket.IdRiderTicket)
            {
                return BadRequest();
            }

            _context.Entry(riderTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiderTicketExists(id))
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

        // POST: api/RiderTickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RiderTicket>> PostRiderTicket(RiderTicket riderTicket)
        {
          if (_context.RiderTickets == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.RiderTickets'  is null.");
          }
            _context.RiderTickets.Add(riderTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRiderTicket", new { id = riderTicket.IdRiderTicket }, riderTicket);
        }

        // DELETE: api/RiderTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRiderTicket(int? id)
        {
            if (_context.RiderTickets == null)
            {
                return NotFound();
            }
            var riderTicket = await _context.RiderTickets.FindAsync(id);
            if (riderTicket == null)
            {
                return NotFound();
            }

            _context.RiderTickets.Remove(riderTicket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RiderTicketExists(int? id)
        {
            return (_context.RiderTickets?.Any(e => e.IdRiderTicket == id)).GetValueOrDefault();
        }
    }
}
