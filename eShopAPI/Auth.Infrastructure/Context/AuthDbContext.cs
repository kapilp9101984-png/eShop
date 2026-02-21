using Microsoft.EntityFrameworkCore;
using Auth.Domain.Entity;

namespace Auth.Infrastructure.Context
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserInRole> UserInRoles { get; set; } = null!;
        public DbSet<Features> Features { get; set; } = null!;
        public DbSet<FeatureInRole> FeatureInRoles { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<PasswordResetTokens> PasswordResetTokens { get; set; } = null!;
        public DbSet<EmailVerificationTokens> EmailVerificationTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Default to a local SQLite database if no options provided
                optionsBuilder.UseSqlite("Data Source=auth.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add entity configuration here if needed
        }
    }
}
