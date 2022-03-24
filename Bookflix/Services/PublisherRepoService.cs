using Bookflix.Models;
using Bookflix.Models.Context;

namespace Bookflix.Services
{
    public class PublisherRepoService: IPublisherRepository
    {
        public BookflixDbContext Context { get; set; }

        public PublisherRepoService(BookflixDbContext context)
        {
            Context = context;
        }

        public List<Publisher> GetAll()
        {
            return Context.Publishers.ToList();
        }

        public Publisher GetDetails(int id)
        {
            return Context.Publishers.Find(id);
        }

        public void Insert(Publisher pub)
        {
            Context.Publishers.Add(pub);
            Context.SaveChanges();
        }

        public void UpdatePub(int id, Publisher pub)
        {
            Publisher pubUpdated = Context.Publishers.Find(id);
            pubUpdated.Name = pub.Name;
            pubUpdated.Address = pub.Address;

            Context.SaveChanges();
        }

        public void DeletePub(int id)
        {
            Context.Remove(Context.Publishers.Find(id));
            Context.SaveChanges();
        }
    }
}
