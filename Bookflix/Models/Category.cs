using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookflix.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        //[InverseProperty(nameof(BookCategory.Book))]
        public virtual ICollection<BookCategory>? Books { get; set; }
    }
}
