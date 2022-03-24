using Bookflix.Models;

namespace Bookflix.Services
{
    public interface IPublisherRepository
    {
        public List<Publisher> GetAll();
        public Publisher GetDetails(int id);
        public void Insert(Publisher pub);
        public void UpdatePub(int id, Publisher pub);
        public void DeletePub(int id);
    }
}
