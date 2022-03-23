using System.ComponentModel.DataAnnotations;

namespace Bookflix.Models
{
	public class SoldBook
	{
		public int BookISBN { get; set; }
		public Book Book { get; set; }
		[DataType(DataType.Date)]
		public DateTime SellDate { get; set; }
	}
}
