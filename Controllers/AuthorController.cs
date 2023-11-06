using Microsoft.AspNetCore.Mvc;
using DSCC.CW1._14973.API.Models;
using Microsoft.EntityFrameworkCore;
using DSCC.CW1._14973.API.Data;

namespace DSCC.CW1._14973.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthorController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            if (_db.Authors == null)
            {
                return NotFound();
            }
            return await _db.Authors.ToListAsync();
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            if (_db.Authors == null)
            {
                return NotFound();
            }
            var author = await _db.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Author/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _db.Entry(author).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsAuthor(id))
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

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            if (_db.Authors == null)
            {
                return Problem("Entity set 'AuthorContext.Authors'  is null.");
            }
            _db.Authors.Add(author);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_db.Authors == null)
            {
                return NotFound();
            }
            var author = await _db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        

        private bool IsAuthor(int id)
        {
            return (_db.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
