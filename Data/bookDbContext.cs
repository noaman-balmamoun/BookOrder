using Microsoft.EntityFrameworkCore;
using BookOrder.Models;

namespace BookOrder.Data;

public class bookDbContext:DbContext
{
    public bookDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }    
    public DbSet<User> Users { get; set; }  

}
