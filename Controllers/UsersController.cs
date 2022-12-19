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

namespace BookOrder.Controllers
{
    public class UsersController : Controller
    {
        private readonly bookDbContext _context;

        public UsersController(bookDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
       

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string na, string pa)
        {
            SqlConnection conn1 = new SqlConnection("Server=.;Database=BOOK;Trusted_Connection=true;Encrypt=false");
            string sql;
            sql = "SELECT * FROM Users where name ='" + na + "' and  pass ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string role = (string)reader["role"];
                string id = Convert.ToString((int)reader["Id"]);
                HttpContext.Session.SetString("Name", na);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("Id", id);
                reader.Close();
                conn1.Close();
                if (role == "customer")
                    return RedirectToAction("Index", "Books");

                else
                    return RedirectToAction("Users", "login");

            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,pass,email")] User user)
           
        {
            user.role = "customer";

            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(login));
        }
         

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit()

        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("Id"));



            var user = await _context.Users.FindAsync(id);
           
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,pass,role,email")] User user)
        {
            

            if (ModelState.IsValid)
            {
               
                    _context.Update(user);
                   await _context.SaveChangesAsync();
                
                 
                
                return RedirectToAction(nameof(login));
            }
            return View(user);
        }

        // GET: Users/Delete/5
       
        private bool UserExists(int id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
}
