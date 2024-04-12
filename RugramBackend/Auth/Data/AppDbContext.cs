using Auth.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; } = null!;
	public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
	public DbSet<MailConfirmationToken> MailConfirmationTokens { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}