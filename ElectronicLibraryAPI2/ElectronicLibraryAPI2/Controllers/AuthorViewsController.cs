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
    public class AuthorViewsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public AuthorViewsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/AuthorViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorView>>> GetAuthorViews()
        {
          if (_context.AuthorViews == null)
          {
              return NotFound();
          }
            return await _context.AuthorViews.ToListAsync();
        }

        // GET: api/AuthorViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorView>> GetAuthorView(int id)
        {
          if (_context.AuthorViews == null)
          {
              return NotFound();
          }
            var authorView = await _context.AuthorViews.FindAsync(id);

            if (authorView == null)
            {
                return NotFound();
            }

            return authorView;
        }

        // PUT: api/AuthorViews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorView(int id, AuthorView authorView)
        {
            if (id != authorView.IdAuthorView)
            {
                return BadRequest();
            }

            _context.Entry(authorView).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorViewExists(id))
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

        // POST: api/AuthorViews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorView>> PostAuthorView(AuthorView authorView)
        {
          if (_context.AuthorViews == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.AuthorViews'  is null.");
          }
            _context.AuthorViews.Add(authorView);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthorView", new { id = authorView.IdAuthorView }, authorView);
        }


        [HttpGet("GetAuthorViewIdByBookId/{bookId}")]
        public async Task<ActionResult<int>> GetAuthorViewIdByBookId(int bookId)
        {
            if (_context.AuthorViews == null)
            {
                return NotFound();
            }

            var authorView = await _context.AuthorViews
                .FirstOrDefaultAsync(av => av.BookId == bookId);

            if (authorView == null)
            {
                return NotFound();
            }

            return authorView.IdAuthorView;
        }



        // DELETE: api/AuthorViews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorView(int id)
        {
            if (_context.AuthorViews == null)
            {
                return NotFound();
            }
            var authorView = await _context.AuthorViews.FindAsync(id);
            if (authorView == null)
            {
                return NotFound();
            }

            _context.AuthorViews.Remove(authorView);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorViewExists(int id)
        {
            return (_context.AuthorViews?.Any(e => e.IdAuthorView == id)).GetValueOrDefault();
        }
    }
}
