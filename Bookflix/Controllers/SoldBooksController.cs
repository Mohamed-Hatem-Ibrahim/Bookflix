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

namespace Bookflix.Controllers
{
    public class SoldBooksController : Controller
    {
        private readonly BookflixDbContext _context;

        public SoldBooksController(BookflixDbContext context)
        {
            _context = context;
        }

        // GET: SoldBooks
        public async Task<IActionResult> Index()
        {
            var bookflixDbContext = _context.SoldBooks.Include(s => s.Book);
            return View(await bookflixDbContext.ToListAsync());
        }

        // GET: SoldBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldBook = await _context.SoldBooks
                .Include(s => s.Book)
                .FirstOrDefaultAsync(m => m.BookISBN == id);
            if (soldBook == null)
            {
                return NotFound();
            }

            return View(soldBook);
        }

        // GET: SoldBooks/Create
        public IActionResult Create()
        {
            ViewData["BookISBN"] = new SelectList(_context.Books, "ISBN", "Title");
            return View();
        }

        // POST: SoldBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookISBN,SellDate")] SoldBook soldBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookISBN"] = new SelectList(_context.Books, "ISBN", "Title", soldBook.BookISBN);
            return View(soldBook);
        }

        // GET: SoldBooks/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var soldBook = await _context.SoldBooks.FindAsync(id);
        //    if (soldBook == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["BookISBN"] = new SelectList(_context.Books, "ISBN", "Description", soldBook.BookISBN);
        //    return View(soldBook);
        //}

        // POST: SoldBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("BookISBN,SellDate")] SoldBook soldBook)
        //{
        //    if (id != soldBook.BookISBN)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(soldBook);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SoldBookExists(soldBook.BookISBN))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BookISBN"] = new SelectList(_context.Books, "ISBN", "Description", soldBook.BookISBN);
        //    return View(soldBook);
        //}

        // GET: SoldBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldBook = await _context.SoldBooks
                .Include(s => s.Book)
                .FirstOrDefaultAsync(m => m.BookISBN == id);
            if (soldBook == null)
            {
                return NotFound();
            }

            return View(soldBook);
        }

        // POST: SoldBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soldBook = await _context.SoldBooks.FindAsync(id);
            _context.SoldBooks.Remove(soldBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoldBookExists(int id)
        {
            return _context.SoldBooks.Any(e => e.BookISBN == id);
        }
    }
}
