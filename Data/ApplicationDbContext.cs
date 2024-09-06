using DBMS_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DBMS_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Payment> Payment { get; set; } // Add DbSet for Payment entity
        
        public DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Wallet and ApplicationUser
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithOne()
                .HasForeignKey<Wallet>(w => w.UserId);


            base.OnModelCreating(modelBuilder);

            // Define the relationship between Payment and ApplicationUser
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .IsRequired();
            
            
            base.OnModelCreating(modelBuilder);

            // Define the relationship between Payment and ApplicationUser
            modelBuilder.Entity<Cart>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .IsRequired();
        }


        public override int SaveChanges()
        {
            // Intercept the creation of new ApplicationUser entities
            foreach (var entry in ChangeTracker.Entries<IdentityUser>())
            {
                if (entry.State == EntityState.Added)
                {
                    // Create a corresponding entry in the Wallet table
                    var wallet = new Wallet
                    {
                        UserId = entry.Entity.Id,
                        Balance = 2000 // Default balance
                    };
                    Wallets.Add(wallet);
                }
            }

            return base.SaveChanges();
        }



    }
}

  
       
    
    
    
    
