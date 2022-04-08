
using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bookflix.Data.Cart
{
    public class ShoppingCart
    {

        public List<ShoppingCartItem> shoppingCartItems { get; set; }


        

    }
}
