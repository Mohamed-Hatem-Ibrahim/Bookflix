using Bookflix.Areas.Admin.Models;
using Bookflix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookflix.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/home")]

    public class AuthorsController : Controller
    {
        private IAuthorRepository Repository;
        public AuthorsController(IAuthorRepository repo)
        {
            Repository = repo;
        }
        // GET: AuthorsController
        [Route("index")]
        public ActionResult Index()
        {
            return View(Repository.GetAuthors());
        }

        // GET: AuthorsController/Details/5
        [Route("Details")]
        public ActionResult Details(int id)
        {
            Author author = Repository.GetAuthorByID(id);
            return View(author);
        }

        // GET: AuthorsController/Create
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Route("Create")]
        public ActionResult Create(Author author)
        {
            try
            {
                Repository.InsertAuthor(author);
                Repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorsController/Edit/5
        [Route("Edit")]
        public ActionResult Edit(int id)
        {
            Author author = Repository.GetAuthorByID(id);
            return View(author);
        }

        // POST: AuthorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                Repository.UpdateAuthor(author);
                Repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorsController/Delete/5
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            Author author = Repository.GetAuthorByID(id);
            return View(author);
        }

        // POST: AuthorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Delete")]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                Repository.DeleteAuthor(id);
                Repository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
