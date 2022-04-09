using Bookflix.Data.Cart;
using Bookflix.Models;
using Bookflix.Services;
using Bookflix.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookflix.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {

        private readonly IRepository<Book> repository;
        private readonly IOrderService orderService;
        ICartUtility utility;
        UserManager<ApplicationUser> userManager;
        public OrdersController(IRepository<Book> Repository, IOrderService OrderService, ICartUtility cartUtility, UserManager<ApplicationUser> _userManager)
        {
            repository = Repository;
            orderService = OrderService;
            utility = cartUtility;
            userManager = _userManager;
        }
        public IActionResult ShoppingCart()
        {
            var response = new ShoppingCart() { shoppingCartItems = utility.GetShopingCart().shoppingCartItems };
            return View(response);
        }

        public RedirectToActionResult AddItemToShoppingCart(int id)
        {
            var book = repository.GetDetails(id);

            if (book != null)
            {
                utility.AddItemToCart(book);

            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public RedirectToActionResult RemoveAllAmountOfElement(int id,int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                RemoveItemFromShoppingCart(id);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }
        public RedirectToActionResult RemoveItemFromShoppingCart(int id)
        {
            var book = repository.GetDetails(id);

            if (book != null)
            {
                utility.RemoveItemFromCart(book);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }


        public IActionResult CompleteOrder()
        {
            var items = utility.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            utility.ClearCart();

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Book.StockNo -= items[i].Amount;
                repository.Update(items[i].Book.ISBN, items[i].Book);
            }
            orderService.StoreOrder(items, userId, userEmailAddress);
            return View("OrderCompleted");
        }

        public IActionResult Index()
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = orderService.GetOrdersByUserIdAndRole(userId, userRole);

            var users = new List<ApplicationUser>();
            foreach (var order in orders)
            {
                users.Add(userManager.FindByIdAsync(order.UserId).Result);
            }

            ViewBag.users = users;
            return View(orders);
        }
    }
}
