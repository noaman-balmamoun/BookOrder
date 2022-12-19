using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookOrder.Data;
using BookOrder.Models;
using Microsoft.Data.SqlClient;
using static NuGet.Packaging.PackagingConstants;

namespace BookOrder.Controllers
{
    public class OrdersController : Controller
    {
        private readonly bookDbContext _context;

        public OrdersController(bookDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
              return View(await _context.Orders.ToListAsync());
        }
        public async Task<IActionResult> Create(int? id)
        {
            var book = await _context.Books.FindAsync(id);

            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId, int quantity)

        {
            Orders order = new Orders();
            order.bookId = bookId;
            order.quantity = quantity;

            order.userid = Convert.ToInt32(HttpContext.Session.GetString("userid")); ;
            order.orderdate = DateTime.Today;
            _context.Add(order);
            await _context.SaveChangesAsync();

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\aiman\\OneDrive\\Documents\\mynewdb.mdf;Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "UPDATE book  SET bookquantity  = bookquantity   - '" + order.quantity + "'  where (id ='" + order.bookId + "' )";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();


            return RedirectToAction(nameof(myorders));



        }







        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
           

            return View(order);
        }


         
        
           

        






        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           

            var order = await _context.Orders.FindAsync(id);
           
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,bookId,userid,quantity,orderdate")] Order order)
        {
            

            if (ModelState.IsValid)
            {
                
                    _context.Update(order);
                    await _context.SaveChangesAsync();
               
                
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
           

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return _context.Orders.Any(e => e.Id == id);
        }
    }
}
