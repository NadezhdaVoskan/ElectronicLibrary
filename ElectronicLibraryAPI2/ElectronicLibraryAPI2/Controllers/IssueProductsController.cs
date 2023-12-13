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
    public class IssueProductsController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public IssueProductsController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/IssueProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueProduct>>> GetIssueProducts()
        {
          if (_context.IssueProducts == null)
          {
              return NotFound();
          }
            return await _context.IssueProducts.ToListAsync();
        }

        // GET: api/IssueProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IssueProduct>> GetIssueProduct(int id)
        {
          if (_context.IssueProducts == null)
          {
              return NotFound();
          }
            var issueProduct = await _context.IssueProducts.FindAsync(id);

            if (issueProduct == null)
            {
                return NotFound();
            }

            return issueProduct;
        }

        // PUT: api/IssueProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssueProduct(int id, IssueProduct issueProduct)
        {
            if (id != issueProduct.IdIssueProduct)
            {
                return BadRequest();
            }

            _context.Entry(issueProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueProductExists(id))
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

        // POST: api/IssueProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IssueProduct>> PostIssueProduct(IssueProduct issueProduct)
        {
          if (_context.IssueProducts == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.IssueProducts'  is null.");
          }
            _context.IssueProducts.Add(issueProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIssueProduct", new { id = issueProduct.IdIssueProduct }, issueProduct);
        }

        // DELETE: api/IssueProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssueProduct(int id)
        {
            if (_context.IssueProducts == null)
            {
                return NotFound();
            }
            var issueProduct = await _context.IssueProducts.FindAsync(id);
            if (issueProduct == null)
            {
                return NotFound();
            }

            _context.IssueProducts.Remove(issueProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IssueProductExists(int id)
        {
            return (_context.IssueProducts?.Any(e => e.IdIssueProduct == id)).GetValueOrDefault();
        }
    }
}
