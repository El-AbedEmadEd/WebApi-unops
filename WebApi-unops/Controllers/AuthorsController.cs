using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_unops.Model;

namespace WebApi_unops.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public AuthorsController(BookStoreDbContext context)
        {
            _context = context;
        }



        [HttpPost]
        public async Task<ActionResult<BookItem>> InsertAuthors([FromForm] Authors author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Authors>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

    }
}
