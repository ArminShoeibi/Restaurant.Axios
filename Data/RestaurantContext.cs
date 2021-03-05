using System;
using Microsoft.EntityFrameworkCore;
using Restaurant.Axios.Domain;


namespace Restaurant.Axios.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>(food =>
            {
                food.Property(f => f.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                food.Property(f => f.PhotoPath)
                    .HasMaxLength(50);

                food.Property(f => f.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                food.Property(f => f.Ingredients)
                    .IsRequired()
                    .HasMaxLength(200);

                food.Property(f => f.Nationality)
                    .IsRequired()
                    .HasMaxLength(30);
            });
        }
    }
}
