using Microsoft.EntityFrameworkCore;
using Todos.WebAPI.Models;

namespace Todos.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }
}
