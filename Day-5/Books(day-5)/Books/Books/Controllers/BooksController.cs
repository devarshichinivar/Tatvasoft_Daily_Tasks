using DataAccessLayer.BooksService;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = "Administrator")]
        public ActionResult<List<Book>> Get() => _booksService.GetAll();

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Book> Get(int id)
        {
            var currentUser = GetCurrentUser();
            if(currentUser != null)
            {
                return Ok($"Hi {currentUser.GivenName}, you are a {currentUser.Role}");
            }

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
        public IActionResult Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            var existingBook = _booksService.GetById(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            _booksService.Update(book);
            return NoContent();
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

        private UserInfo GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserInfo
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
