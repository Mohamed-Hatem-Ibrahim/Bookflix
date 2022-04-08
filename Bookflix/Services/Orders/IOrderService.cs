using Bookflix.Models;

namespace Bookflix.Services.Orders
{
    public interface IOrderService
    {
        void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        List<Order> GetOrdersByUserIdAndRole(string userId, string userRole);
    }
}
