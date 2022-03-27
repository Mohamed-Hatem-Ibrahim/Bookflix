using Bookflix.Models;

namespace Bookflix.Areas.Admin.Models
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthorByID(int authorId);
        void InsertAuthor(Author author);
        void DeleteAuthor(int authorID);
        void UpdateAuthor(Author author);
        void Save();
    }
}
