using Bookflix.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Bookflix.Data.ViewComponents
{
    public class ShoppingCartCount :ViewComponent
    {
  
            private readonly ShoppingCart shoppingCart;

            public ShoppingCartCount(ShoppingCart ShoppingCart)
            {
                shoppingCart = ShoppingCart;
            }

            public IViewComponentResult Invoke()
            {
                var items = shoppingCart.GetShoppingCartItems();

                return View(items.Count);
            }
        }
    
}
