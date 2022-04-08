using Bookflix.Models;

namespace Bookflix.Data.Cart
{
    public interface ICartUtility
    {
        public ShoppingCart GetShopingCart();


        public void AddItemToCart(Book book);

        public void RemoveItemFromCart(Book book);


        public List<ShoppingCartItem> GetShoppingCartItems();


        public decimal GetShoppingCartTotal();

        public void ClearCart();
    }
}
