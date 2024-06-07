using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.BooksService
{
    public class BooksService
    {
        private readonly AppDbContext _cIDbContext;

        public BooksService(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }

        public List<Book> GetAll()
        {
            return _cIDbContext.Books.ToList();
        }

        public Book GetById(int id) => _cIDbContext.Books.FirstOrDefault(b => b.Id == id);
        public void Add(Book book)
        {
            _cIDbContext.Books.Add(book);
            _cIDbContext.SaveChanges();
        }

        public async Task<Book> UpdateBookAsync(int bookId, Book updatedBook)
        {
            // Retrieve the book by ID
            var book = await _cIDbContext.Books.FindAsync(bookId);
            if (book == null)
            {
                // Handle the case when the book is not found
                return null;
            }

            // Update the book's properties
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            

            // Save the changes to the database
            await _cIDbContext.SaveChangesAsync();

            return book;
        }

        public void Delete(int id)
        {
            var book = _cIDbContext.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _cIDbContext.Books.Remove(book);
                _cIDbContext.SaveChanges();
            }
        }
    }
}
