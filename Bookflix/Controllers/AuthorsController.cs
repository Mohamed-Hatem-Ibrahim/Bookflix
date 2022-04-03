using Bookflix.Models;
using Bookflix.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookflix.Controllers
{
    public class AuthorsController : Controller
    {
        private IRepository<Author> Repository;

        public AuthorsController(IRepository<Author> repository)
        {
            Repository = repository;
        }

        public IActionResult Index()
        {
            return View(Repository.GetAll());
        }

        public IActionResult Details(int id)
        {
            Author author = Repository.GetDetails(id);
            return View(author);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            try
            {
                Repository.Insert(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Author author = Repository.GetDetails(id);
            return View(author);
        }

        // POST: AuthorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Author author)
        {
            try
            {
                Repository.Update(id, author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorsController/Delete/5
        public IActionResult Delete(int id)
        {
            Author author = Repository.GetDetails(id);
            return View(author);
        }

        // POST: AuthorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Author author)
        {
            try
            {
                Repository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
