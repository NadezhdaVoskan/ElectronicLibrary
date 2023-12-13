using ElectronicLibraryAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicLibraryAPI2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReturnBooksController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public ReturnBooksController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ReasonReturn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnBook>>> GetReturnBooks()
        {
            if (_context.ReturnBooks == null)
            {
                return NotFound();
            }
            return await _context.ReturnBooks.ToListAsync();
        }

        // GET: api/ReasonReturn/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnBook>> GetReturnBook(int id)
        {
            if (_context.ReturnBooks == null)
            {
                return NotFound();
            }
            var returnBook = await _context.ReturnBooks.FindAsync(id);

            if (returnBook == null)
            {
                return NotFound();
            }

            return returnBook;
        }

        // PUT: api/ReasonReturn/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReturnBook(int id, ReturnBook returnBook)
        {
            if (id != returnBook.IdReturnBook)
            {
                return BadRequest();
            }

            _context.Entry(returnBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnBookExists(id))
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

        // POST: api/ReasonReturn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReturnBook>> PostReturnBook(ReturnBook returnBook)
        {
            if (_context.ReturnBooks == null)
            {
                return Problem("Entity set 'Library_DatabaseContext.ReturnBooks'  is null.");
            }
            _context.ReturnBooks.Add(returnBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReturnBook", new { id = returnBook.IdReturnBook }, returnBook);
        }

        // DELETE: api/ReasonReturn/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReturnBook(int id)
        {
            if (_context.ReturnBooks == null)
            {
                return NotFound();
            }
            var returnBooks = await _context.ReturnBooks.FindAsync(id);
            if (returnBooks == null)
            {
                return NotFound();
            }

            _context.ReturnBooks.Remove(returnBooks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReturnBookExists(int id)
        {
            return (_context.ReturnBooks?.Any(e => e.IdReturnBook == id)).GetValueOrDefault();
        }
    }
}
