using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly BookflixDbContext _dbContext;

        public OrderService(BookflixDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Order>> GetOrdersByUserIdAsync(string UserId)
        {
            return _dbContext.Orders.Include(n=> n.OrderItems).ThenInclude(n=>n.Book).Where(n=>n.UserId == UserId)
                .ToListAsync();
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,

            };
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    BookId = item.Book.ISBN,
                    OrderId = order.Id,
                    Price = item.Book.Price,
                };
               await _dbContext.OrderItems.AddAsync(orderItem);

            }

            await _dbContext.SaveChangesAsync();    
        }
    }

}
