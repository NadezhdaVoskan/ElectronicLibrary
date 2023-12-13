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
    public class TypeLiteratureViewsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public TypeLiteratureViewsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TypeLiteratureViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeLiteratureView>>> GetTypeLiteratureViews()
        {
          if (_context.TypeLiteratureViews == null)
          {
              return NotFound();
          }
            return await _context.TypeLiteratureViews.ToListAsync();
        }


        [HttpGet("GetTypeLiteratureViewIdByBookId/{bookId}")]
        public async Task<ActionResult<int>> GetTypeLiteratureViewIdByBookId(int bookId)
        {
            if (_context.TypeLiteratureViews == null)
            {
                return NotFound();
            }

            var typeLiteratureView = await _context.TypeLiteratureViews
                .FirstOrDefaultAsync(av => av.BookId == bookId);

            if (typeLiteratureView == null)
            {
                return NotFound();
            }

            return typeLiteratureView.IdTypeLiteratureView;
        }

        // GET: api/TypeLiteratureViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeLiteratureView>> GetTypeLiteratureView(int id)
        {
          if (_context.TypeLiteratureViews == null)
          {
              return NotFound();
          }
            var typeLiteratureView = await _context.TypeLiteratureViews.FindAsync(id);

            if (typeLiteratureView == null)
            {
                return NotFound();
            }

            return typeLiteratureView;
        }

        // PUT: api/TypeLiteratureViews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeLiteratureView(int id, TypeLiteratureView typeLiteratureView)
        {
            if (id != typeLiteratureView.IdTypeLiteratureView)
            {
                return BadRequest();
            }

            _context.Entry(typeLiteratureView).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeLiteratureViewExists(id))
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

        // POST: api/TypeLiteratureViews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeLiteratureView>> PostTypeLiteratureView(TypeLiteratureView typeLiteratureView)
        {
          if (_context.TypeLiteratureViews == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.TypeLiteratureViews'  is null.");
          }
            _context.TypeLiteratureViews.Add(typeLiteratureView);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeLiteratureView", new { id = typeLiteratureView.IdTypeLiteratureView }, typeLiteratureView);
        }

        // DELETE: api/TypeLiteratureViews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeLiteratureView(int id)
        {
            if (_context.TypeLiteratureViews == null)
            {
                return NotFound();
            }
            var typeLiteratureView = await _context.TypeLiteratureViews.FindAsync(id);
            if (typeLiteratureView == null)
            {
                return NotFound();
            }

            _context.TypeLiteratureViews.Remove(typeLiteratureView);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeLiteratureViewExists(int id)
        {
            return (_context.TypeLiteratureViews?.Any(e => e.IdTypeLiteratureView == id)).GetValueOrDefault();
        }
    }
}
