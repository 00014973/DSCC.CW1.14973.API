using Microsoft.AspNetCore.Mvc;
using DSCC.CW1._14973.API.Models;
using DSCC.CW1._14973.API.Services;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using DSCC.CW1._14973.API.Data;

namespace DSCC.CW1._14973.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BookController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (_db.Books == null)
            {
                return NotFound();
            }
            return await _db.Books.Include(p => p.Author).ToListAsync();
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (_db.Books == null)
            {
                return NotFound();
            }

            //var books = await _db.Books.Include(_ => _.Author).ToListAsync();

            var book = await _db.Books.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST: api/Book
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] BookDTO bookDto)
        {
            if (_db.Books == null)
            {
                return Problem("Entity set 'AuthorContext.Books'  is null.");
            }

            // Find the author by name
            var author = await _db.Authors.FindAsync(bookDto.AuthorId);


            // new Book using the DTO and found or created Author's ID
            var book = new Book
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Author = author
            };

            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO dto)
        {
            try
            {
                var foundBook = await _db.Books.FindAsync(id);
                if (foundBook == null)
                {
                    return NotFound();
                }
                foundBook.Description = dto.Description;
                foundBook.Title = dto.Title;
                foundBook.Author = await _db.Authors.FindAsync(dto.AuthorId);
                
                await _db.SaveChangesAsync();
                return Ok(foundBook);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

       

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_db.Books == null)
            {
                return NotFound();
            }
            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
