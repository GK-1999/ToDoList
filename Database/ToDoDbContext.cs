using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Database
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ToDoModel> ToDoList { get; set; }
    }
}
