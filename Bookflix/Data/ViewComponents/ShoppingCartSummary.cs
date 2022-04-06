
using Bookflix.Data.Cart;
using Bookflix.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookflix.Data.ViewComponents
{
    
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartSummary(ShoppingCart ShoppingCart)
        {
            shoppingCart = ShoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.shoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShopingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);

            return View(items);
        }
    }
}
