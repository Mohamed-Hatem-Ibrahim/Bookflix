using Bookflix.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Bookflix.Data.ViewComponents
{
    public class ShoppingCartCount :ViewComponent
    {
  
            private readonly ICartUtility utility;

            public ShoppingCartCount(ICartUtility cartUtility)
            {
                utility = cartUtility;
            }

            public IViewComponentResult Invoke()
            {


                return View(utility.GetShoppingCartItems().Count);
            }
        }
    
}
