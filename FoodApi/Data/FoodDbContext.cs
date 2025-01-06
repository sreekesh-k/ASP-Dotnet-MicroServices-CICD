using FoodApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodApi.Data
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) { }

    public DbSet<Food> foods { get; set; }
    }
}
