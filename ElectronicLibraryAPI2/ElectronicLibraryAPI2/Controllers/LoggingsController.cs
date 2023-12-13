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
    public class LoggingsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public LoggingsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Loggings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logging>>> GetLoggings()
        {
             try
            {
                var loggings = await _context.Loggings
                    .Include(p => p.IssueProduct)
                    .Include(p => p.User)
                    .Include(p => p.RiderTicket)
                    .Include(p => p.StatusLogging)
                    .ToListAsync();

                if (loggings == null)
                {
                    return NotFound();
                }

                return loggings;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET: api/Loggings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Logging>> GetLogging(int id)
        {
          if (_context.Loggings == null)
          {
              return NotFound();
          }
            var logging = await _context.Loggings.FindAsync(id);

            if (logging == null)
            {
                return NotFound();
            }

            return logging;
        }

        // PUT: api/Loggings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogging(int id, Logging logging)
        {
            if (id != logging.IdLogging)
            {
                return BadRequest();
            }

            _context.Entry(logging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoggingExists(id))
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

        // POST: api/Loggings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Logging>> PostLogging(Logging logging)
        {
          if (_context.Loggings == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.Loggings'  is null.");
          }
            _context.Loggings.Add(logging);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogging", new { id = logging.IdLogging }, logging);
        }

        // DELETE: api/Loggings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogging(int id)
        {
            if (_context.Loggings == null)
            {
                return NotFound();
            }
            var logging = await _context.Loggings.FindAsync(id);
            if (logging == null)
            {
                return NotFound();
            }

            _context.Loggings.Remove(logging);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoggingExists(int id)
        {
            return (_context.Loggings?.Any(e => e.IdLogging == id)).GetValueOrDefault();
        }
    }
}
