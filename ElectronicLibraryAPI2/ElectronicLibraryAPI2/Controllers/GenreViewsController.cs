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
    public class GenreViewsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public GenreViewsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/GenreViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreView>>> GetGenreViews()
        {
          if (_context.GenreViews == null)
          {
              return NotFound();
          }
            return await _context.GenreViews.ToListAsync();
        }

        // GET: api/GenreViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreView>> GetGenreView(int id)
        {
          if (_context.GenreViews == null)
          {
              return NotFound();
          }
            var genreView = await _context.GenreViews.FindAsync(id);

            if (genreView == null)
            {
                return NotFound();
            }

            return genreView;
        }

        [HttpGet("GetGenreViewIdByBookId/{bookId}")]
        public async Task<ActionResult<int>> GetGenreViewIdByBookId(int bookId)
        {
            if (_context.GenreViews == null)
            {
                return NotFound();
            }

            var genreView = await _context.GenreViews
                .FirstOrDefaultAsync(av => av.BookId == bookId);

            if (genreView == null)
            {
                return NotFound();
            }

            return genreView.IdGenreView;
        }


        // PUT: api/GenreViews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenreView(int id, GenreView genreView)
        {
            if (id != genreView.IdGenreView)
            {
                return BadRequest();
            }

            _context.Entry(genreView).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreViewExists(id))
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

        // POST: api/GenreViews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GenreView>> PostGenreView(GenreView genreView)
        {
          if (_context.GenreViews == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.GenreViews'  is null.");
          }
            _context.GenreViews.Add(genreView);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenreView", new { id = genreView.IdGenreView }, genreView);
        }

        // DELETE: api/GenreViews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreView(int id)
        {
            if (_context.GenreViews == null)
            {
                return NotFound();
            }
            var genreView = await _context.GenreViews.FindAsync(id);
            if (genreView == null)
            {
                return NotFound();
            }

            _context.GenreViews.Remove(genreView);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreViewExists(int id)
        {
            return (_context.GenreViews?.Any(e => e.IdGenreView == id)).GetValueOrDefault();
        }
    }
}
