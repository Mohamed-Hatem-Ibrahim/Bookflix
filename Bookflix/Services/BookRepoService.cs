using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Services
{
    public class BookRepoService : IRepository<Book>
    {
        private readonly BookflixDbContext Context;

        public BookRepoService(BookflixDbContext context)
        {
            Context = context;
        }

        public List<Book> GetAll()
        {
            return Context.Books.Include(p => p.Publisher).Include(a => a.Author).Include(c => c.Categories).ToList();
        }

        public Book? GetDetails(int? _isbn)
        {
            return Context.Books.Find(_isbn);
        }

        public void Insert(Book? book)
        {
            if (book == null)
                return;
            Context.Books.Add(book);
            Context.SaveChanges();
        }

        public void Update(int id, Book? book)
        {
            Book? bookUpdated = GetDetails(id);
            if(bookUpdated == null || book == null)
                return;
            bookUpdated.Description = book.Description;
            bookUpdated.PagesNo = book.PagesNo;
            bookUpdated.Title = book.Title;
            bookUpdated.Price = book.Price;
            bookUpdated.StockNo = book.StockNo;
            bookUpdated.AuthorID = book.AuthorID;
            bookUpdated.PublisherID = book.PublisherID;

            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            Book? book = GetDetails(id);
            if (book == null)
                return;
            Context.Remove(book);
            Context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return Context.Books.Any(e => e.ISBN == id);
        }
    }
}
