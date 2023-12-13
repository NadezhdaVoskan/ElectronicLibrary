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
    public class TypeLiteraturesController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public TypeLiteraturesController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TypeLiteratures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeLiterature>>> GetTypeLiteratures()
        {
          if (_context.TypeLiteratures == null)
          {
              return NotFound();
          }
            return await _context.TypeLiteratures.ToListAsync();
        }

        // GET: api/TypeLiteratures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeLiterature>> GetTypeLiterature(int id)
        {
          if (_context.TypeLiteratures == null)
          {
              return NotFound();
          }
            var typeLiterature = await _context.TypeLiteratures.FindAsync(id);

            if (typeLiterature == null)
            {
                return NotFound();
            }

            return typeLiterature;
        }

        // PUT: api/TypeLiteratures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeLiterature(int id, TypeLiterature typeLiterature)
        {
            if (id != typeLiterature.IdTypeLiterature)
            {
                return BadRequest();
            }

            _context.Entry(typeLiterature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeLiteratureExists(id))
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

        // POST: api/TypeLiteratures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeLiterature>> PostTypeLiterature(TypeLiterature typeLiterature)
        {
          if (_context.TypeLiteratures == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.TypeLiteratures'  is null.");
          }
            _context.TypeLiteratures.Add(typeLiterature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeLiterature", new { id = typeLiterature.IdTypeLiterature }, typeLiterature);
        }

        // DELETE: api/TypeLiteratures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeLiterature(int id)
        {
            if (_context.TypeLiteratures == null)
            {
                return NotFound();
            }
            var typeLiterature = await _context.TypeLiteratures.FindAsync(id);
            if (typeLiterature == null)
            {
                return NotFound();
            }

            _context.TypeLiteratures.Remove(typeLiterature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeLiteratureExists(int id)
        {
            return (_context.TypeLiteratures?.Any(e => e.IdTypeLiterature == id)).GetValueOrDefault();
        }
    }
}
