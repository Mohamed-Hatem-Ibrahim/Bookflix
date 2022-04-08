using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Services
{
    public class SoldBookRepoService : IRepository<SoldBook>
    {
        private BookflixDbContext Context;
        public SoldBookRepoService(BookflixDbContext context)
        {
            Context = context;
        }
        public void Insert(SoldBook? soldBook)
        {
            if (soldBook == null)
                return;
            Context.SoldBooks.Add(soldBook);
            Context.SaveChanges();
            //throw new NotImplementedException();
        }
        public List<SoldBook> GetAll()
        {
            return Context.SoldBooks.ToList();
        }
        public SoldBook? GetDetails(int? id)
        {
            throw new NotImplementedException();

            //return Context.SoldBooks.Include(x=>x.BookISBN).FirstOrDefault(sb=>sb.i);
        }
        public void DeleteComposite(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        ///////////??????????????????????????????------------how----------------???????????????????????????????????????????????
        ///
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id) 
        {
            throw new NotImplementedException();
            //return Context.SoldBooks
        }

        public void Update(int id, SoldBook? t)
        {
            throw new NotImplementedException();
        }

    }
}
