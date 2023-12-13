using ElectronicLibraryAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ElectronicLibraryAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderCardController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public ReaderCardController(Library_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetBooksByRiderTicket")]
        public ActionResult<string> GetBooksByRiderTicket(string numberRiderTicket)
        {
            var result = _context.GetBooksByReaderTicket(numberRiderTicket).FirstOrDefault();

            if (result == null)
            {
                return NotFound("No books found for the provided rider ticket number.");
            }

            return Ok(result.Books);
        }



    }
}
