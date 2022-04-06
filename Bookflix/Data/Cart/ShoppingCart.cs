using Bookflix.Models;
using Bookflix.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookflix.Data.Cart
{
    public class ShoppingCart
    {
        public BookflixDbContext context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }
        public ShoppingCart(BookflixDbContext _context)
        {
            context = _context;
        }

        public static ShoppingCart GetShopingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<BookflixDbContext>();
            string cartId = session.GetString("CartId") ??  Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId =  cartId };
            //,shoppingCartItems = GetShoppingCartItems(cartId,context)
        }
        public void AddItemToCart(Book book)
        {
            var shoppingCartItem = context.ShoppingCartItems.FirstOrDefault(i=>i.Book.ISBN == book.ISBN &&
            i.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new()
                {
                    ShoppingCartId = ShoppingCartId,
                    Book = book,
                    Amount = 1
                };
                context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            context.SaveChanges();
        }

        public void RemoveItemFromCart(Book book)
        {
            var shoppingCartItem = context.ShoppingCartItems.FirstOrDefault(i => i.Book.ISBN == book.ISBN &&
            i.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
               if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            context.SaveChanges();
        }

        public async Task ClearShoppingAsync()
        {
            var items = await context.ShoppingCartItems.Where(i =>
            i.ShoppingCartId == ShoppingCartId).ToListAsync();

            context.ShoppingCartItems.RemoveRange(items);

            await context.SaveChangesAsync();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return shoppingCartItems ?? (shoppingCartItems = context.ShoppingCartItems.Where(i =>
            i.ShoppingCartId == ShoppingCartId).Include(i => i.Book).ToList());
        }

        public static List<ShoppingCartItem> GetShoppingCartItems(string id, BookflixDbContext _context)
        {
            List<ShoppingCartItem> ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == id)
                .Include(n => n.Book)
               // .ThenInclude(p => p.Media)
                .ToList();

            return ShoppingCartItems;
        }

        public decimal GetShoppingCartTotal()
        {
            return context.ShoppingCartItems.Where(i => i.ShoppingCartId == ShoppingCartId).Select(i=>i.Book.Price * i.Amount).Sum();
        }


    }
}
