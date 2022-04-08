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
        public IWebHostEnvironment WebHostEnvironment { get; }

        public BookController(IRepository<Book> _BookRepo, IRepository<Author> authorRepo, IRepository<Publisher> publiserRepo, IWebHostEnvironment webHostEnvironment)
        {
            BookRepo = _BookRepo;
            AuthorRepo = authorRepo;
            PubliserRepo = publiserRepo;
            WebHostEnvironment = webHostEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(BookRepo.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            var foundBooks = BookRepo.GetAll().Where(x => x.Title.IndexOf(search.Trim().ToLower(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();
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
