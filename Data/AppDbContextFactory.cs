// Data/AppDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

// namespace firstApp.Data
// {
    public class AppDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext >
    {
        public ApplicationDbContext  CreateDbContext(string[] args)
        {
            // Set up configuration sources.
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext >();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new ApplicationDbContext (builder.Options);
        }
    }
// }
