using System.ComponentModel.DataAnnotations;

namespace Bookflix.Models
{
    public class ShoppingCartItem
    {

        public Book Book { get; set; }

        public int Amount { get; set; }


    }
}
