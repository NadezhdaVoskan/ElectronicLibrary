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
    public class StatusLoggingsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public StatusLoggingsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StatusLoggings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusLogging>>> GetStatusLoggings()
        {
          if (_context.StatusLoggings == null)
          {
              return NotFound();
          }
            return await _context.StatusLoggings.ToListAsync();
        }

        // GET: api/StatusLoggings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusLogging>> GetStatusLogging(int id)
        {
          if (_context.StatusLoggings == null)
          {
              return NotFound();
          }
            var statusLogging = await _context.StatusLoggings.FindAsync(id);

            if (statusLogging == null)
            {
                return NotFound();
            }

            return statusLogging;
        }

        // PUT: api/StatusLoggings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusLogging(int id, StatusLogging statusLogging)
        {
            if (id != statusLogging.IdStatusLogging)
            {
                return BadRequest();
            }

            _context.Entry(statusLogging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusLoggingExists(id))
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

        // POST: api/StatusLoggings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusLogging>> PostStatusLogging(StatusLogging statusLogging)
        {
          if (_context.StatusLoggings == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.StatusLoggings'  is null.");
          }
            _context.StatusLoggings.Add(statusLogging);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusLogging", new { id = statusLogging.IdStatusLogging }, statusLogging);
        }

        // DELETE: api/StatusLoggings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusLogging(int id)
        {
            if (_context.StatusLoggings == null)
            {
                return NotFound();
            }
            var statusLogging = await _context.StatusLoggings.FindAsync(id);
            if (statusLogging == null)
            {
                return NotFound();
            }

            _context.StatusLoggings.Remove(statusLogging);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusLoggingExists(int id)
        {
            return (_context.StatusLoggings?.Any(e => e.IdStatusLogging == id)).GetValueOrDefault();
        }
    }
}
