using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Areas.Admin.Models
{
    public class AuthorRepository : IAuthorRepository
    {
        private BookflixDbContext context;

        public AuthorRepository(BookflixDbContext context)
        {
            this.context = context;
        }
        public void DeleteAuthor(int authorID)
        {
           Author author = context.Authors.Find(authorID);
            context.Authors.Remove(author);
        }

        public Author GetAuthorByID(int authorId)
        {
            return context.Authors.Find(authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return context.Authors.ToList();
        }

        public void InsertAuthor(Author author)
        {
            context.Authors.Add(author);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            context.Entry(author).State = EntityState.Modified;
        }
    }
}
