using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RefHubApp.Models;

namespace RefHubApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MainEntity> mainEntities { get; set; }

        public DbSet<AppUser> appUsers { get; set; }
    }
}
