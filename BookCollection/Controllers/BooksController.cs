using BookCollection.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookCollection.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks( string author = null, int? year = null, string publisher = null)
        {
            IQueryable<Book> query = _context.Books;

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Contains(author));
            }

            if (year.HasValue)
            {
                query = query.Where(b => b.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(publisher))
            {
                query = query.Where(b => b.Publisher != null && b.Publisher.Contains(publisher));
            }

            var books = query.ToList();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
            {
                return BadRequest(ModelState);
            }

            if (_context.Books.Any(b => b.Title == book.Title && b.Author == book.Author))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok(new { id = book.Id });
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {

            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return NoContent();
        }
    }

}
