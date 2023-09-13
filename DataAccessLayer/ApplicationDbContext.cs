using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SiteAccount> SiteAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SiteAccount>()
                .HasOne<AppUser>()
                .WithMany(u => u.SiteAccounts)
                .HasForeignKey(sa => sa.UserId)
                .IsRequired();

            modelBuilder.Entity<SiteAccount>()
                .HasOne<AppUser>()
                .WithMany(u => u.SiteAccounts)
                .HasForeignKey(sa => sa.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
