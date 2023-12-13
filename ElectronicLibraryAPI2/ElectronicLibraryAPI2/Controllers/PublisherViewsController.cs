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
    public class PublisherViewsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public PublisherViewsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PublisherViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherView>>> GetPublisherViews()
        {
          if (_context.PublisherViews == null)
          {
              return NotFound();
          }
            return await _context.PublisherViews.ToListAsync();
        }

        [HttpGet("GetPublisherViewIdByBookId/{bookId}")]
        public async Task<ActionResult<int>> GetPublisherViewIdByBookId(int bookId)
        {
            if (_context.PublisherViews == null)
            {
                return NotFound();
            }

            var publisherView = await _context.PublisherViews
                .FirstOrDefaultAsync(av => av.BookId == bookId);

            if (publisherView == null)
            {
                return NotFound();
            }

            return publisherView.IdPublisherView;
        }


        // GET: api/PublisherViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherView>> GetPublisherView(int id)
        {
          if (_context.PublisherViews == null)
          {
              return NotFound();
          }
            var publisherView = await _context.PublisherViews.FindAsync(id);

            if (publisherView == null)
            {
                return NotFound();
            }

            return publisherView;
        }

        // PUT: api/PublisherViews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisherView(int id, PublisherView publisherView)
        {
            if (id != publisherView.IdPublisherView)
            {
                return BadRequest();
            }

            _context.Entry(publisherView).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherViewExists(id))
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

        // POST: api/PublisherViews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublisherView>> PostPublisherView(PublisherView publisherView)
        {
          if (_context.PublisherViews == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.PublisherViews'  is null.");
          }
            _context.PublisherViews.Add(publisherView);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisherView", new { id = publisherView.IdPublisherView }, publisherView);
        }

        // DELETE: api/PublisherViews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisherView(int id)
        {
            if (_context.PublisherViews == null)
            {
                return NotFound();
            }
            var publisherView = await _context.PublisherViews.FindAsync(id);
            if (publisherView == null)
            {
                return NotFound();
            }

            _context.PublisherViews.Remove(publisherView);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherViewExists(int id)
        {
            return (_context.PublisherViews?.Any(e => e.IdPublisherView == id)).GetValueOrDefault();
        }
    }
}
