using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auth.Infrastructure.Context
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();

            // Prefer environment variable, fall back to local sqlite file
            var connectionString = Environment.GetEnvironmentVariable("AUTH_CONNECTION") ?? "Data Source=auth.db";
            optionsBuilder.UseSqlite(connectionString);

            return new AuthDbContext(optionsBuilder.Options);
        }
    }
}
