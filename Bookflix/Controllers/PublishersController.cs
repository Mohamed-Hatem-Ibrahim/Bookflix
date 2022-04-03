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
    public class PublishersController : Controller
    {
        public IRepository<Publisher> PublisherRepository { get; set; }

        public PublishersController(IRepository<Publisher> publisherRepository)
        {
            PublisherRepository = publisherRepository;
        }

        // GET: Publishers
        public ActionResult Index()
        {
            return View(PublisherRepository.GetAll());
        }

        // GET: Publishers/Details/5
        public ActionResult Details(int id)
        {
            return View(PublisherRepository.GetDetails(id));
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Publisher pub)
        {
            try
            {
                PublisherRepository.Insert(pub);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Publishers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Publisher pub)
        {
            try
            {
                PublisherRepository.Update(id, pub);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Publishers/Delete/5
        public ActionResult Delete(int id)
        {
            return View(PublisherRepository.GetDetails(id));
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                PublisherRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //private bool PublisherExists(int id)
        //{
        //    return _context.Publishers.Any(e => e.ID == id);
        //}
    }
}
