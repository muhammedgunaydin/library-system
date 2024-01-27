using lib_project.Models;
using Microsoft.EntityFrameworkCore;

public class LibraryDbContext : DbContext
{

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Rack> Racks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserBookRelation> UserBookRelations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=test;Database=libauto");
    }
}

