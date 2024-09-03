using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;


namespace InventorySystem.Models
{
    public class InventoryContext : DbContext // derived from dbcontext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { // public inventory context established, with base options
        }

        public DbSet<Inventory> Inventories { get; set; } = null!; // inventories dbset created
        public DbSet<Category> Categories{ get; set; } = null!; // categories dbset created
        public DbSet<Status> Statuses{ get; set; } = null!; // statuses dbset created

        protected override void OnModelCreating(ModelBuilder modelBuilder) // onmodelcreating modelbuilder override method
        {
            modelBuilder.Entity<Category>().HasData( //Category Entity data populated:
                new Category { CategoryId = "food", Name = "Food" },
                new Category { CategoryId = "clothing", Name = "Clothing" },
                new Category { CategoryId = "electronics", Name = "Electronics" },
                new Category { CategoryId = "hd", Name = "Home & Decor" },
                new Category { CategoryId = "games", Name = "Games" }
                );
            modelBuilder.Entity<Status>().HasData( //Status Entity data populated:
                new Status { StatusId = "accurate", Name = "Accurate" },
                new Status { StatusId = "notacc", Name = "Not Accurate" }
                );
        }
    }
}
