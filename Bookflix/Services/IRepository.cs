using Bookflix.Models;

namespace Bookflix.Services
{
    public interface IRepository <T>
    {
        public List<T> GetAll();
        public T? GetDetails(int? id);
        public void Insert(T? t);
        public void Update(int id, T? t);
        public void Delete(int id);
        public bool Exists (int id);
    }
}
