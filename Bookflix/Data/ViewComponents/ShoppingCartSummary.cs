
using Bookflix.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Bookflix.Data.ViewComponents
{
    
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ICartUtility _cartUtility;

        public ShoppingCartSummary(ICartUtility utility)
        {
            _cartUtility = utility;
        }

        public IViewComponentResult Invoke()
        {

            return View(_cartUtility.GetShopingCart());
        }
    }
}
