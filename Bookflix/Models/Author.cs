using System.ComponentModel.DataAnnotations;

namespace Bookflix.Models
{
	public class Author
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		public string Address { get; set; }

		public virtual ICollection<Book> Books { get; set; }
	}
}
