using Microsoft.EntityFrameworkCore;
using UsersCollectionAPI.Model.Entities;

namespace UsersCollectionAPI.Model.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public required DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<User>().HasQueryFilter(item => item.Status != Status.Deleted);
	}
}
