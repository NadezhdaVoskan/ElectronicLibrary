using ElectronicLibraryAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace ElectronicLibraryAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookListsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public BookListsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BookLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookList>>> GetBookLists()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var bookLists = await _context.BookLists.ToListAsync();
            var bookListsJson = JsonSerializer.Serialize(bookLists, options);
            return Content(bookListsJson, "application/json");
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<BookList>> GetBookList(string title)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var bookList = await _context.BookLists.FirstOrDefaultAsync(b => b.Название == title);

            if (bookList == null)
            {
                return NotFound();
            }

            var bookListJson = JsonSerializer.Serialize(bookList, options);
            return Content(bookListJson, "application/json");
        }

        // GET: api/BookLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookList>> GetBookList(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var bookList = await _context.BookLists.FindAsync(id);

            if (bookList == null)
            {
                return NotFound();
            }

            var bookListJson = JsonSerializer.Serialize(bookList, options);
            return Content(bookListJson, "application/json");
        }

        // Other CRUD operations for BookList can be implemented here

        private bool BookListExists(string bookTitle)
        {
            return _context.BookLists.Any(e => e.Название == bookTitle);
        }
    }
}
