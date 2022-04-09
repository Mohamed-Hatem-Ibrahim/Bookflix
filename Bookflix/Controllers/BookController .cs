#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookflix.Models;
using Bookflix.Models.Context;
using Bookflix.Services;

namespace Bookflix.Controllers
{
    public class BookController : Controller
    {
        public IRepository<Book> BookRepo { get; set; }
        public IRepository<Author> AuthorRepo { get; set; }
        public IRepository<Publisher> PubliserRepo { get; }
        public IRepository<Category> CategoryRepo { get; set; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public BookController(
            IRepository<Book> _BookRepo,
            IRepository<Author> authorRepo,
            IRepository<Publisher> publiserRepo,
            IRepository<Category> categoryRepo,
            IWebHostEnvironment webHostEnvironment)
        {
            BookRepo = _BookRepo;
            AuthorRepo = authorRepo;
            PubliserRepo = publiserRepo;
            CategoryRepo = categoryRepo;
            WebHostEnvironment = webHostEnvironment;
        }

        // GET: Books

        private List<SelectListItem> GetEnumValues(Type type)
        {
            List<SelectListItem> values = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(type))
            {
                values.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
                });
            }
            return values;
        }

        private List<SelectListItem> GetEnumValues(Type type, string[] existing)
        {
            List<SelectListItem> values = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(type))
            {
                values.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString(),
                    Selected = existing.Contains(item.ToString())
                });
            }
            return values;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string[] publishingtypess, string[] covertypess, string[] booktypess, int activePage = 1)
        {
            List<Book> foundBooks = new();
            List<Book> allBooks = BookRepo.GetAll();
            foundBooks.AddRange(allBooks.Where(x => booktypess.Any(bt => bt.ToString() == x.BookType.ToString())).ToList());
            foundBooks.AddRange(allBooks.Where(x => covertypess.Any(ct => ct.ToString() == x.CoverType.ToString())).ToList());
            foundBooks.AddRange(allBooks.Where(x => publishingtypess.Any(pt => pt.ToString() == x.PublishingType.ToString())).ToList());
            foundBooks = foundBooks.Distinct().ToList();
            if (publishingtypess.Length == 0 && covertypess.Length == 0 && booktypess.Length == 0)
                foundBooks = allBooks;


            var booktypes = GetEnumValues(typeof(BookType), booktypess);
            var covertypes = GetEnumValues(typeof(CoverType), covertypess);
            var publishingtypes = GetEnumValues(typeof(PublishingType), publishingtypess);
            var categories = new List<SelectListItem>();
            foreach (var category in CategoryRepo.GetAll())
            {
                categories.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.ID.ToString(),
                });
            }

            ViewBag.booktypes = booktypes;
            ViewBag.covertypes = covertypes;
            ViewBag.publishingtypes = publishingtypes;
            ViewBag.categories = categories;

            double theCount = foundBooks.Count;
            double divisor = 6;
            int numberOfPages = int.Parse(Math.Ceiling(theCount / divisor).ToString());

            ViewBag.activePage = activePage;
            ViewBag.NumberOfPages = numberOfPages;

            
            foundBooks = foundBooks.Skip((activePage - 1) * (int)divisor).Take((int)divisor).ToList();

            return View(foundBooks);
        }

        [HttpPost]
        public IActionResult Index(string[] categories, string[] publishingtypes, string[] covertypes, string[] booktypes, string search, int activePage = 1)
        {

            List<Book> foundBooks = new();
            var allBooks = BookRepo.GetAll();
            foundBooks.AddRange(allBooks.Where(x => booktypes.Any(bt => bt.ToString() == x.BookType.ToString())).ToList());
            foundBooks.AddRange(allBooks.Where(x => covertypes.Any(ct => ct.ToString() == x.CoverType.ToString())).ToList());
            foundBooks.AddRange(allBooks.Where(x => publishingtypes.Any(pt => pt.ToString() == x.PublishingType.ToString())).ToList());
            foundBooks.AddRange(allBooks.Where(x => categories.Any(c => x.Categories.Any(cx => cx.CategoryID.ToString() == c))).ToList());
            if (search != null)
                foundBooks.AddRange(allBooks.Where(x => x.Title.IndexOf(search?.Trim()?.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0).ToList());
            foundBooks = foundBooks.Distinct().ToList();


            var booktypesss = GetEnumValues(typeof(BookType), booktypes);
            var covertypesss = GetEnumValues(typeof(CoverType), covertypes);
            var publishingtypesss = GetEnumValues(typeof(PublishingType), publishingtypes);
            var categoriesss = new List<SelectListItem>();
            foreach (var category in CategoryRepo.GetAll())
            {
                categoriesss.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.ID.ToString(),
                    Selected = categories.Any(x => x == category.ID.ToString())
                });
            }

            ViewBag.booktypes = booktypesss;
            ViewBag.covertypes = covertypesss;
            ViewBag.publishingtypes = publishingtypesss;
            ViewBag.categories = categoriesss;
            if (categories.Length == 0 && publishingtypes.Length == 0 && covertypes.Length == 0 && booktypes.Length == 0 && string.IsNullOrEmpty(search))
                foundBooks = allBooks;

            double theCount = foundBooks.Count;
            double divisor = 6;
            int numberOfPages = int.Parse(Math.Ceiling(theCount / divisor).ToString());

            ViewBag.activePage = activePage;
            ViewBag.NumberOfPages = numberOfPages;


            allBooks = allBooks.Skip((activePage - 1) * (int)divisor).Take((int)divisor).ToList();

            return View(foundBooks);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = BookRepo.GetDetails(id);
            if (book == null)
            {
                return NotFound();
            }
            var categories = CategoryRepo.GetAll();
            var includedCategories = categories.Where(i => book.Categories.Any(x => x.CategoryID == i.ID)).ToList();
            ViewBag.Categories = includedCategories;
            return PartialView("_DetailsPopUp", book);
        }
    }
}
