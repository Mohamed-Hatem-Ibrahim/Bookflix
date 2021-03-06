using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly BookflixDbContext _dbContext;
        private UserManager<ApplicationUser> Manager;
        public OrderService(BookflixDbContext dbContext,UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            Manager = userManager;
        }

        public List<Order> GetOrdersByUserIdAndRole(string UserId, string userRole)
        {
            var orders = _dbContext.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Book)
                .ToList();

            var user = Manager.FindByIdAsync(UserId).Result;
            bool isAdmin =  Manager.IsInRoleAsync(user, "Admin").Result;
            
            if (!isAdmin)
            {
                orders = orders.Where(i => i.UserId == UserId).ToList();

            }
            return orders;
        }

        public async Task StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
                OrderDate = DateTime.Now

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
