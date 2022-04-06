using Bookflix.Data.Cart;
using Bookflix.Data.ViewModels;
using Bookflix.Models;
using Bookflix.Services;
using Bookflix.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Bookflix.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IRepository<Book> repository;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrderService orderService;
        public OrdersController(IRepository<Book> Repository, ShoppingCart ShopingCart,IOrderService OrderService)
        {
            repository = Repository;
            shoppingCart = ShopingCart;
            orderService = OrderService;
        }
        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.shoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShopingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }

        public RedirectToActionResult AddItemToShoppingCart(int id)
        {
            var book = repository.GetDetails(id);

            if(book != null)
            {
                shoppingCart.AddItemToCart(book);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public RedirectToActionResult RemoveItemFromShoppingCart(int id)
        {
            var book = repository.GetDetails(id);

            if (book != null)
            {
                shoppingCart.RemoveItemFromCart(book);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }


        public async Task  <IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await shoppingCart.ClearShoppingAsync();
            return View("OrderCompleted");
        }
    }
}
