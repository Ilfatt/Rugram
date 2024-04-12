using Microsoft.EntityFrameworkCore;
using Profile.Data.Models;

namespace Profile.Data;

public class AppDbContext : DbContext
{	
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<UserProfile> UserProfiles { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}