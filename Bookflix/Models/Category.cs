using System.ComponentModel.DataAnnotations;

namespace Bookflix.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Book>? Books { get; set; }
    }
}
