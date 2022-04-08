using Bookflix.Models;
using Bookflix.Models.Context;

namespace Bookflix.Services
{
    public class CategoryRepoService : IRepository<Category>
    {
        private readonly BookflixDbContext Context;

        public CategoryRepoService(BookflixDbContext context)
        {
            Context = context;
        }
        public void Delete(int id)
        {
            Category? category = GetDetails(id);
            if(category == null)
                return;
            Context.Categories.Remove(category);
            Context.SaveChanges();
        }

        public void DeleteComposite(int id1, int id2)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            return Context.Categories.Any(e => e.ID == id);
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public Category? GetDetails(int? id)
        {
            return Context.Categories.Find(id);
        }

        public void Insert(Category? category)
        {
            if(category == null)
                return ;
            Context.Categories.Add(category);
            Context.SaveChanges();
        }

        public void Update(int id, Category? category)
        {
            Category? oldCategory = GetDetails(id);
            if (oldCategory == null || category == null)
                return;
            oldCategory.Name = category.Name;
            oldCategory.Books = category.Books;
            oldCategory.ID = category.ID;

            Context.SaveChanges();
        }
    }
}
