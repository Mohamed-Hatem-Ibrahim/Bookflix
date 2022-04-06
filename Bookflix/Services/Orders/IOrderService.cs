using Bookflix.Models;

namespace Bookflix.Services.Orders
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items,string userId,string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
