using System.ComponentModel.DataAnnotations;

namespace Bookflix.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public decimal Price { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }


        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
