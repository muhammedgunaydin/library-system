using lib_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lib_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController: ControllerBase
    {
        private readonly LibraryDbContext _context;

        public AuthorController(LibraryDbContext context)
        {
            _context = context;
        }

        //Get all authors - GET: api/author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        //Get author by id - GET: api/author/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }
            return author;
        }

        //Create author - POST: api/author
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor",new {id = author.Id},author);
        }

        //Edit author - PUT: api/author/id
        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> EditAuthor (int id , Author author)
        {
            if(id != author.Id)
            {
                return BadRequest();
            }
            _context.Entry(author).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete author - DELETE: api/author/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if(author == null)
            {
                return NotFound();
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
