using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace XTND_Technical_Assessment.Infra;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var conn = configuration.GetConnectionString("DefaultConnection")
                   ?? "Host=localhost;Port=5432;Database=myapp;Username=postgres;Password=1";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(conn, b => b.MigrationsAssembly("XTND_Technical_Assessment"));

        return new AppDbContext(optionsBuilder.Options);
    }
}