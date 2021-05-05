using CinemaApi.Models;
using CinemaApi.Models.Orders;
using CinemaApi.Models.Snacks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Snack> Snacks { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Movie>()
                .Property(e => e.Category)
                .HasConversion<string>();

            base.OnModelCreating(builder);
        }
    }
}
