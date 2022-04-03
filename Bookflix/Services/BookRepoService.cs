using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Services
{
    public class BookRepoService : IRepository<Book>
    {
        public BookflixDbContext Context { get; set; }

        public BookRepoService(BookflixDbContext context)
        {
            Context = context;
        }

        public List<Book> GetAll()
        {
            return Context.Books.Include(b => b.Publisher).Include(b1 => b1.Author).ToList();
        }

        public Book GetDetails(int _isbn)
        {
            return Context.Books.Find(_isbn);
        }

        public void Insert(Book book)
        {
            Context.Books.Add(book);
            Context.SaveChanges();
        }

        public void Update(int id, Book book)
        {
            Book bookUpdated = Context.Books.Find(id);

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
            Context.Remove(Context.Books.Find(id));
            Context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return Context.Books.Any(e => e.ISBN == id);
        }
    }
}
