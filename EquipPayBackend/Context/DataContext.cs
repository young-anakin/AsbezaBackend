using EquipPayBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipPayBackend.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany()
                .HasForeignKey(ri => ri.IngredientId);
            modelBuilder.Entity<UserAccount>()
                .HasOne(ua => ua.UserInfo)
                .WithOne(ui => ui.UserAccount)
                .HasForeignKey<UserInfo>(ui => ui.UserAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserAccounts)
                .WithOne(ua => ua.Role)
                .HasForeignKey(ua => ua.RoleId);
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Ingredient)
                .WithMany()
                .HasForeignKey(ci => ci.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Recipe)
                .WithMany()
                .HasForeignKey(ci => ci.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Ingredient)
                .WithMany()
                .HasForeignKey(oi => oi.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Recipe)
                .WithMany()
                .HasForeignKey(oi => oi.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }


    }
}
