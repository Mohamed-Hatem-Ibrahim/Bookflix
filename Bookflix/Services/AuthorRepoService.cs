using Bookflix.Models;
using Bookflix.Models.Context;
using Bookflix.Services;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Areas.Admin.Models
{
    public class AuthorRepoService : IRepository<Author>
    {
        private BookflixDbContext context;

        public AuthorRepoService(BookflixDbContext context)
        {
            this.context = context;
        }
        public void Delete(int authorID)
        {
           Author? author = context.Authors.Find(authorID);
            if(author == null)
                return;
            context.Authors.Remove(author);
            context.SaveChanges();
        }

        public Author? GetDetails(int? authorId)
        {
            return context.Authors.Find(authorId);
        }

        public List<Author> GetAll()
        {
            return context.Authors.ToList();
        }

        public void Insert(Author? author)
        {
            if(author == null)
                return ;
            context.Authors.Add(author);
            context.SaveChanges();
        }


        public void Update(int id, Author? author)
        {
            if(author == null)
                return ;
            context.Entry(author).State = EntityState.Modified;
            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return context.Authors.Any(a => a.ID == id);
        }

        public void DeleteComposite(int id1, int id2)
        {
            throw new NotImplementedException();
        }
    }
}
