using Bookflix.Data;
using Bookflix.Models;
using Bookflix.Models.Context;
using Bookflix.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bookflix.Controllers
{
    public class DashboardController : Controller
    {
        public IRepository<Book> BookRepo { get; set; }
        public IRepository<Author> AuthorRepo { get; set; }
        public IRepository<Publisher> PubliserRepo { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        private readonly BookflixDbContext Context;
        private readonly ApplicationDbContext Context2;
        public DashboardController(IRepository<Book> _BookRepo, IRepository<Author> authorRepo, IRepository<Publisher> publiserRepo, IWebHostEnvironment webHostEnvironment, BookflixDbContext context, ApplicationDbContext context2)
        {
            BookRepo = _BookRepo;
            AuthorRepo = authorRepo;
            PubliserRepo = publiserRepo;
            WebHostEnvironment = webHostEnvironment;
            Context = context;
            Context2 = context2;
        }
        public async Task<IActionResult> Panel()
        {
            //number of users
            ViewData["userCount"] = Context2.Users.ToList().Count();
            List<ApplicationUser> numOfUsers = Context2.Users.ToList();
            //int superadminCount = 0;
            //foreach (var i in numOfUsers)
            //{
            //    if (i.UserName == "superadmin")
            //        superadminCount++;
            //}
            //ViewData["superadminCount"] = superadminCount;
            int userCount = 0;
            foreach (var i in numOfUsers)
            {
                userCount++;
            }
            ViewData["userCount"] = (userCount - 1);

            //IdentityUserRole<string>iur=Context2.UserRoles.ToList();

            //ViewBag.customeOrders =  Context.OrderItems.ToList();

            //number of order
            List<OrderItem> orderItems = Context.OrderItems.ToList();
            ViewData["orders"] = orderItems;
            ViewData["ordersCount"] = orderItems.Count();
            List<decimal> totalPriceEveryOrder = new List<decimal>();
            decimal totalRevenue = 0;

            foreach (var i in orderItems)
            {
                totalPriceEveryOrder.Add(i.Amount * i.Price);
            }
            //ViewData["total_amount"] = totalPriceEveryOrder;
            for (int i = 0; i < totalPriceEveryOrder.Count; i++)
            {
                totalRevenue += totalPriceEveryOrder[i];
            }
            ViewData["revenue"] = totalRevenue;

            //most selling book
            List<Book> bookItems = Context.Books.ToList();
            //ViewData["bookItems"] = bookItems;
            ViewData["booksCount"] = bookItems.Count();

            //Each Index represent amount for each book
            List<int> amountEachBook = new List<int>();
            foreach (var i in bookItems)
            {
                int AmountBooks = 0;
                foreach (var j in orderItems)
                {
                    if (j.BookId == i.ISBN)
                    {
                        AmountBooks += j.Amount;
                    }
                }
                amountEachBook.Add(AmountBooks);
            }
            ViewData["total_amount_each_book"] = amountEachBook;
            //All Transaction
            List<Order> allOrders = Context.Orders.ToList();
            List<int> allYears = new List<int>();
            foreach (var item in allOrders)
            {
                allYears.Add(item.OrderDate.Year);
            }
            ViewData["allYears"] = allYears;
            return View(bookItems);

            //return View(BookRepo.GetAll());
        }
    }
}
