using eScout_v2.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace eScout_v2.Application.Infrastructure.Database.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : DbContext(options)
{
    private readonly string _schema = configuration.GetSection("ConnectionStrings")["DbSchema"] ?? "public";
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activity", _schema);
        });
    }
}