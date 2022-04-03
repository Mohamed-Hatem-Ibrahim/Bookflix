using Bookflix.Models;
using Bookflix.Models.Context;
using Bookflix.Services;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Areas.Admin.Models
{
    public class AuthorRepository : IRepository<Author>
    {
        private BookflixDbContext context;

        public AuthorRepository(BookflixDbContext context)
        {
            this.context = context;
        }
        public void Delete(int authorID)
        {
           Author author = context.Authors.Find(authorID);
            context.Authors.Remove(author);
            context.SaveChanges();
        }

        public Author GetDetails(int authorId)
        {
            return context.Authors.Find(authorId);
        }

        public List<Author> GetAll()
        {
            return context.Authors.ToList();
        }

        public void Insert(Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();
        }


        public void Update(int id, Author author)
        {
            context.Entry(author).State = EntityState.Modified;
            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return context.Authors.Any(a => a.ID == id);
        }
    }
}
