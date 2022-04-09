using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Services
{
    public class BookCategoriesRepoService : IRepository<BookCategory>
    {
        private BookflixDbContext context;
        public BookCategoriesRepoService(BookflixDbContext context)
        {
            this.context = context;
        }
        public void Delete(int id)
        {
            List<BookCategory> list = new();
            var all = context.BookCategories.AsNoTracking().ToList();
            foreach (var bookcat in all)
                if(bookcat.ISBN == id)
                    list.Add(bookcat);
            context.BookCategories.RemoveRange(list);
            context.SaveChanges();
        }

        public void DeleteComposite(int id1, int id2)
        {
            var bookcategory = context.BookCategories.FirstOrDefault(x => x.ISBN == id1 && x.CategoryID == id2);
            if (bookcategory == null)
                return;
            context.BookCategories.Remove(bookcategory);
            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public List<BookCategory> GetAll()
        {
            return context.BookCategories.ToList();
        }

        public BookCategory? GetDetails(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Category?> GetCategories(int id)
        {
            List<BookCategory> bookCats = context.BookCategories.Include(x=>x.Category).Where(b=>b.ISBN == id).ToList();
            List<Category?> categories = new List<Category?>();
            foreach (var bookCat in bookCats)
            {
                categories.Add(bookCat.Category);
            }
            return categories;
        }

        public void Insert(BookCategory? t)
        {
            if (t == null)
                return;
            context.BookCategories.AddAsync(t);
            context.SaveChanges();
        }

        public void Update(int id, BookCategory? t)
        {
            throw new NotImplementedException();
        }
    }
}
