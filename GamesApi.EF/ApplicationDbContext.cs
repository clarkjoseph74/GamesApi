using GamesApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.EF;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {  
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
         .HasMany(x => x.Games)
         .WithOne(x => x.CategoryType)
         .HasForeignKey(x => x.CategoryId)
         .HasPrincipalKey(x => x.CatId);
    }
    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<GamePlatform> GamesPlatforms { get; set; }
}
