using Bookflix.Models;
using Newtonsoft.Json;

namespace Bookflix.Data.Cart
{
    public class CartUtility :ICartUtility
    {
        IHttpContextAccessor _session;
        public CartUtility(IHttpContextAccessor session)
        {
            _session = session;
        }

        //deserialize
        public ShoppingCart GetShopingCart()
        {
            ShoppingCart? shoppingCart;
            string str = _session?.HttpContext?.Session?.GetString("Cart");
            if(str != null)
            {
             shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(str);

            }
            else { 
                shoppingCart = new ShoppingCart()
                {
                    shoppingCartItems = new List<ShoppingCartItem>()
                };
                PutShopingCart(shoppingCart);
            }
            return shoppingCart;
        }

        //serialize
        private void PutShopingCart(ShoppingCart shoppingCart)
        {
            var cartJSON = JsonConvert.SerializeObject(shoppingCart, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            _session.HttpContext.Session.SetString("Cart", cartJSON);
        }


        public void AddItemToCart(Book book)
        {

                ShoppingCart shoppingCart = GetShopingCart();
                var shoppingCartItems = shoppingCart.shoppingCartItems;

                int index = shoppingCartItems.FindIndex(m => m.Book.ISBN == book.ISBN);

                if (index == -1)
                {
                    shoppingCartItems.Add(
                    new()
                    {
                        Book = book,
                        Amount = 1
                    });
                }
                else
                {
                    shoppingCartItems[index].Amount++;
                }

                PutShopingCart(shoppingCart);
            
        }

        public void RemoveItemFromCart(Book book)
        {
            ShoppingCart shoppingCart = GetShopingCart();
            var shoppingCartItems = shoppingCart.shoppingCartItems;

            int index = shoppingCartItems.FindIndex(m => m.Book.ISBN == book.ISBN);

            if (shoppingCartItems[index].Amount == 1)
            {
                shoppingCartItems.Remove(shoppingCartItems[index]);
            }
            else
            {
                shoppingCartItems[index].Amount--;
            }

            PutShopingCart(shoppingCart);
        }


        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return GetShopingCart().shoppingCartItems;
        }


        public decimal GetShoppingCartTotal()
        {
            ShoppingCart shoppingCart = GetShopingCart();
            var shoppingCartItems = shoppingCart.shoppingCartItems;

            return shoppingCartItems.Select(i => i.Book.Price * i.Amount).Sum();
        }

        public void ClearCart()
        {
            _session.HttpContext?.Session.Remove("Cart");
        }
    }
}


