using Bookflix.Models;
using Bookflix.Models.Context;
using Bookflix.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bookflix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Book> _bookRepository;
    
        public HomeController(ILogger<HomeController> logger, IRepository<Book> bookRepo)
        {
            _logger = logger;
            _bookRepository = bookRepo;
        }

        public IActionResult Index()
        {
            var latestBooks = _bookRepository.GetAll().OrderByDescending(x => x.ISBN).Take(8);
            return View(latestBooks);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}