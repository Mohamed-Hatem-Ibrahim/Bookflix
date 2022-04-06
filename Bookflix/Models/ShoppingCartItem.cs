using System.ComponentModel.DataAnnotations;

namespace Bookflix.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Book Book { get; set; }

        public int Amount { get; set; }
    }
}
