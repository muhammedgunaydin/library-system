
using lib_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lib_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext context)
        {
            _context = context;
        }

        //Get all books- GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            //var books = await _context.Books.Include(x => x.Author).Include(x => x.Rack).OrderBy(x => x.Name).ToListAsync();
            var books = await _context.Books.ToListAsync();
            if(books == null)
            {
                return NotFound();
            }
            return books;

        }

        //Get book by id- GET: api/Book/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        //Create book- POST: api/Book
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            //rack capacity control
            var rack = await _context.Racks.Where(i => i.Id == book.RackId).FirstOrDefaultAsync();
            if (rack != null)
            {
                if (rack.BookCount < rack.Capacity)
                {
                    rack.BookCount++;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var newRack = new Rack
                    {
                        Name = "New rack",
                        Capacity = 6,
                        BookCount = 1
                    };
                    _context.Racks.Add(newRack);
                    await _context.SaveChangesAsync();

                    book.RackId = newRack.Id;
                }
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }


        //Update book- PUT: api/Book/id
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            _context.Entry(book).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete book- DELETE: api/Book/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost(("TakeBook/{id}"))]
        public async Task<ActionResult<Book>> TakeBook([FromRoute] int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            if (!book.isExist)
            {
                return BadRequest("Already book taken");
            }

            var rack = await _context.Racks.FindAsync(book.RackId);
            if (rack != null && rack.BookCount > 0)
            {
                rack.BookCount--;
                // book.RackId = 0; null yapılamıyor neden ? (rack id deki 0 rackın olmayışını temsil ediyor) ve eğer null yapılırsa geri bırakırken nasıl bırakılır
            }

            book.isExist = false;
            await _context.SaveChangesAsync();
            return Ok("Book succesfully take");
        }

        //                          ÖNEMLİ NOT BURAYI SOR(yukarıdaki ve aşağıdaki method id'yi jsondan almıyor neden?)

        [HttpPost("ReturnBook/{id}")]
        public async Task<ActionResult<Book>> ReturnBook([FromRoute] int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            if (book.isExist)
            {
                return BadRequest("Book already in system");
            }

            var rack = await _context.Racks.FindAsync(book.RackId);

            if (rack != null && rack.Capacity < 6)
            {
                rack.BookCount++;
            }

            if (rack == null || rack.BookCount == rack.Capacity)
            {
                var newRack = new Rack
                {
                    Name = "New Rack",
                    Capacity = 6,
                    BookCount = 1
                };
                _context.Racks.Add(newRack);
                await _context.SaveChangesAsync();

                book.RackId = newRack.Id;
            }
            else
            {
                rack.BookCount++;
            }

            book.isExist = true;
            await _context.SaveChangesAsync();
            return Ok("Book returned succesfully");
        }
    }
}
