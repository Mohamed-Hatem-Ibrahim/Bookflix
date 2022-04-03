using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [DisplayName("Book Image")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Uploade Image")]
        public IFormFile ImageFile { get; set; }

        [Required]
        public int StockNo { get; set; }

        public virtual ICollection<Category>?  Categories{ get; set; }

		public int PubID { get; set; }
		public virtual Publisher Publisher{ get; set; }

        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
    }
}
