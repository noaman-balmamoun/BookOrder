using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookOrder.Data;
using BookOrder.Models;

namespace BookOrder.Controllers
{
    public class BooksController : Controller
    {
        private readonly bookDbContext _context;

        public BooksController(bookDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return View(await _context.Books.ToListAsync());
        }
        public async Task<IActionResult> Catalogue()
        {
            return View(await _context.Books.ToListAsync());
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,title,info,bookquantity,price,cataid,author")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           

            var book = await _context.Books.FindAsync(id);
            
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,info,bookquantity,price,cataid,author")] Book book)
        {
           

            if (ModelState.IsValid)
            {
                
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else { 
                
         
            return View(book);
        }
    }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
           

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return _context.Books.Any(e => e.Id == id);
        }
    }
}
