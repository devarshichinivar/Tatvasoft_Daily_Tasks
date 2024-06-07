using DataAccessLayer.BooksService;
using DataAccessLayer.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class BooksController : Controller
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("GetAllBooks")]
        [Authorize]
        public ActionResult<List<Book>> Get() => _booksService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _booksService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public ActionResult<Book> PostBooks(Book book)
        {
            _booksService.Add(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest("Book ID mismatch");
            }

            var book = await _booksService.UpdateBookAsync(id, updatedBook);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _booksService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            _booksService.Delete(id);
            return NoContent();
        }
    }
}
