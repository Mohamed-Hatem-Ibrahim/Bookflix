using Bookflix.Models;
using Bookflix.Models.Context;

namespace Bookflix.Services
{
    public class PublisherRepoService: IRepository <Publisher>
    {
        private readonly BookflixDbContext Context;

        public PublisherRepoService(BookflixDbContext context)
        {
            Context = context;
        }

        public List<Publisher> GetAll()
        {
            return Context.Publishers.ToList();
        }

        public Publisher? GetDetails(int? id)
        {
            return Context.Publishers.Find(id);
        }

        public void Insert(Publisher? pub)
        {
            if (pub == null)
                return;
            Context.Publishers.Add(pub);
            Context.SaveChanges();
        }

        public void Update(int id, Publisher? pub)
        {
            Publisher? pubUpdated = Context.Publishers.Find(id);
            if (pubUpdated == null || pub == null)
                return;
            pubUpdated.Name = pub.Name;
            pubUpdated.Address = pub.Address;

            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            Publisher? publisher = GetDetails(id);
            if (publisher == null)
                return;
            Context.Remove(publisher);
            Context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return Context.Publishers.Any(p => p.ID == id);
        }
    }
}
