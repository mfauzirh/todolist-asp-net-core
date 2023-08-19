using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}
