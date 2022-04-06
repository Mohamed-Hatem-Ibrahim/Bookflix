using System.ComponentModel.DataAnnotations.Schema;

namespace Bookflix.Models
{
    public class BookCategory
    {
        [ForeignKey(nameof(Book))]
        public int ISBN { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryID { get; set; }

        public virtual Book? Book { get; set; }

        public virtual Category? Category { get; set; }
    }
}
