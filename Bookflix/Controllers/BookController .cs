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

        public BookController(IRepository<Book> _BookRepo, IRepository<Author> authorRepo, IRepository<Publisher> publiserRepo, IRepository<Category> categoryRepo, IWebHostEnvironment webHostEnvironment)
        {
            BookRepo = _BookRepo;
            AuthorRepo = authorRepo;
            PubliserRepo = publiserRepo;
            CategoryRepo = categoryRepo;
            WebHostEnvironment = webHostEnvironment;
        }

        //string text = booktype.ToString();
        //int num = (int)Enum.Parse(typeof(BookType), text);
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

        public async Task<IActionResult> Index()
        {
            var booktypes = GetEnumValues(typeof(BookType));

            var covertypes = GetEnumValues(typeof(CoverType));

            var publishingtypes = GetEnumValues(typeof(PublishingType));

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


            return View(BookRepo.GetAll());
        }

        //[ActionName("IndexSearch")]
        //public async Task<IActionResult> Index(string search)
        //{
        //    var foundBooks = BookRepo.GetAll().Where(x => x.Title.IndexOf(search?.Trim()?.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        //    return View(foundBooks);
        //}

        [HttpPost]
        public IActionResult Index(string[] categories, string[] publishingtypes, string[] covertypes, string[] booktypes, string search)
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


            var booktypesss = GetEnumValues(typeof(BookType));
            var covertypesss = GetEnumValues(typeof(CoverType));
            var publishingtypesss = GetEnumValues(typeof(PublishingType));
            var categoriesss = new List<SelectListItem>();
            foreach (var category in CategoryRepo.GetAll())
            {
                categoriesss.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.ID.ToString(),
                });
            }

            ViewBag.booktypes = booktypesss;
            ViewBag.covertypes = covertypesss;
            ViewBag.publishingtypes = publishingtypesss;
            ViewBag.categories = categoriesss;
            if (categories.Length == 0 && publishingtypes.Length == 0 && covertypes.Length == 0 && booktypes.Length == 0 && string.IsNullOrEmpty(search))
                return View(allBooks);
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

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(AuthorRepo.GetAll(), "ID", "Name");
            ViewData["PublisherID"] = new SelectList(PubliserRepo.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ISBN,Title,Description,PublicationYear,Price,PagesNo,ImageFile,StockNo,PublisherID,AuthorID")] Book book)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = WebHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
                string extention = Path.GetExtension(book.ImageFile.FileName);
                book.ImageName = fileName = fileName + book.ISBN.ToString() + extention;
                string path = Path.Combine(wwwRootPath + "/img/books-img/", fileName);

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    await book.ImageFile.CopyToAsync(fs);
                }

                BookRepo.Insert(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(AuthorRepo.GetAll(), "ID", "Name", book.AuthorID);
            ViewData["PublisherID"] = new SelectList(PubliserRepo.GetAll(), "ID", "Name", book.PublisherID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
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
            ViewData["AuthorID"] = new SelectList(AuthorRepo.GetAll(), "ID", "Name", book.AuthorID);
            ViewData["PublisherID"] = new SelectList(PubliserRepo.GetAll(), "ID", "Name", book.PublisherID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ISBN,Title,Description,PublicationYear,Price,PagesNo,ImageName,StockNo,PublisherID,AuthorID")] Book book)
        {
            if (id != book.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    BookRepo.Update(id, book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ISBN))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(AuthorRepo.GetAll(), "ID", "Name", book.AuthorID);
            ViewData["PublisherID"] = new SelectList(PubliserRepo.GetAll(), "ID", "Name", book.PublisherID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
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

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = BookRepo.GetDetails(id);
            var imagePath = Path.Combine(WebHostEnvironment.WebRootPath, "img/books-img", book.ImageName);

            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            BookRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return BookRepo.Exists(id);
        }
    }
}
