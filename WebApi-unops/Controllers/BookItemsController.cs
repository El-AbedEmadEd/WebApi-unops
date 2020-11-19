using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_unops.Model;

namespace WebApi_unops.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookItemsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookItemsController(BookStoreDbContext context)
        {
            _context = context;
        }



        [HttpPost]
        public async Task<ActionResult<BookItem>> InsertBookItem([FromForm] BookItem bookItem)
        {
            _context.BookItems.Add(bookItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookItem), new { id = bookItem.Id }, bookItem);
        }

        [HttpPut]
        public async Task<IActionResult> EditBookItem([FromForm] BookItem bookItem)
        {
            if (bookItem.Id == 0 || bookItem.Id == null)
            {
                return BadRequest();
            }

            _context.Entry(bookItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BookItems.Any(e => e.Id == bookItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetBookItem), new { id = bookItem.Id }, bookItem);
        }

        [HttpDelete]
        public async Task<ActionResult<BookItem>> DeleteBookItem(int id)
        {
            var bookItem = await _context.BookItems.FindAsync(id);
            if (bookItem == null)
            {
                return NotFound();
            }

            _context.BookItems.Remove(bookItem);
            await _context.SaveChangesAsync();

            return bookItem;
        }


     
        public async Task<ActionResult<IList<BookItem>>> FindBookItem(string Title, string Author, string Description)
        {
            if (Title == null && Author == null && Description == null)
                return NotFound();


            var model = await (from x in _context.BookItems
                               where (x.Title.Contains(Title) || Title == "" || Title == null)
                                     && (x.Authors.Name.Contains(Author) || Author == "" || Author == null)
                                        && (x.Description.Contains(Description) || Description == "" || Description == null)
                               select x).ToListAsync();
            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        public async Task<ActionResult<object>> summaryBookItems()
        {

            var countBooks = _context.BookItems.ToList().Count();
            var autorCont = _context.Authors.ToList().Count();

            return new { countBooks , autorCont };
        }



        // GET: api/BookItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookItem>>> GetBookItems()
        {
            return await _context.BookItems.ToListAsync();
        }
        // GET: api/BookItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookItem>> GetBookItem(int id)
        {
            var bookItem = await _context.BookItems.FindAsync(id);

            if (bookItem == null)
            {
                return NotFound();
            }

            return bookItem;
        }

      


    }
}
