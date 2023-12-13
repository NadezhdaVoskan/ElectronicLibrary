using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicLibraryAPI2.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

namespace ElectronicLibraryAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public BooksController(Library_DatabaseContext context)
        {
            _context = context;
        }


        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(
     [FromQuery] string? searchString,
     [FromQuery] string? sortOrder,
     [FromQuery] List<string>? genres,
     [FromQuery] List<string>? types,
     [FromQuery] List<string>? publishers)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            IQueryable<Book> booksQuery = _context.Books
                .Include(b => b.GenreView)
                .ThenInclude(gv => gv.Genre)
                .Include(b => b.TypeLiteratureView)
                .ThenInclude(tlv => tlv.TypeLiterature)
                .Include(b => b.PublisherView)
                .ThenInclude(pv => pv.Publisher)
                .Include(b => b.AuthorView)
                .ThenInclude(av => av.Author);

            if (!string.IsNullOrEmpty(searchString))
            {
                booksQuery = booksQuery.Where(b => b.NameBook.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "price_asc":
                        booksQuery = booksQuery.OrderBy(b => b.Price);
                        break;
                    case "price_desc":
                        booksQuery = booksQuery.OrderByDescending(b => b.Price);
                        break;
                }
            }

            if (genres != null && genres.Any())
            {
                booksQuery = booksQuery.Where(b => b.GenreView.Any(gv => genres.Contains(gv.Genre.NameGenre))).Distinct();
            }

            if (types != null && types.Any())
            {
                booksQuery = booksQuery.Where(b => b.TypeLiteratureView.Any(tlv => types.Contains(tlv.TypeLiterature.NameTypeLiterature))).Distinct();
            }

            if (publishers != null && publishers.Any())
            {
                booksQuery = booksQuery.Where(b => b.PublisherView.Any(pv => publishers.Contains(pv.Publisher.NamePublisher))).Distinct();
            }

            var books = await booksQuery.ToListAsync();
            var booksJson = JsonSerializer.Serialize(books, options);
            return Content(booksJson, "application/json");
        }


        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.IdBook)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.Books'  is null.");
          }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.IdBook }, book);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var book = await _context.Books
                .Include(b => b.GenreView)
                .ThenInclude(gv => gv.Genre)
                .Include(b => b.TypeLiteratureView)
                .ThenInclude(tlv => tlv.TypeLiterature)
                .Include(b => b.PublisherView)
                .ThenInclude(pv => pv.Publisher)
                .Include(b => b.AuthorView)
                .ThenInclude(av => av.Author)
                .FirstOrDefaultAsync(b => b.IdBook == id);

            if (book == null)
            {
                return NotFound(); 
            }

            var bookJson = JsonSerializer.Serialize(book, options);
            return Content(bookJson, "application/json");
        }



        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.IdBook == id)).GetValueOrDefault();
        }
    }
}
