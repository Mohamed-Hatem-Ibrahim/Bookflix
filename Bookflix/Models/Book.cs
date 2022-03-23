using System.ComponentModel.DataAnnotations;

namespace Bookflix.Models
{
    public class Book
    {
        [Key]
        public int ISBN { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime PublicationYear { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int PagesNo { get; set; }

        [Required]
        public int StockNo { get; set; }

        public ICollection<Category>?  Categories{ get; set; }

		public int PubID { get; set; }
		public Publisher Publisher{ get; set; }

        public int AuthorID { get; set; }
        public Author Author { get; set; }
    }
}
