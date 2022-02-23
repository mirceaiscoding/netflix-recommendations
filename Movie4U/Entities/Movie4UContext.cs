using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Entities
{
    public class Movie4UContext : IdentityDbContext
        <User, Role, string, IdentityUserClaim<string>,
        UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        // the constructor
        public Movie4UContext(DbContextOptions<Movie4UContext> options) : base(options) { }

        // entities
        public DbSet<Watcher> Watchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // entities here
            modelBuilder.Entity<Watcher>().ToTable("Watchers");

            // composite keys for relational entities here

            // Relations here
            modelBuilder.Entity<Watcher>()
                .HasOne(watcher => watcher.user)
                .WithOne(user => user.watcher)
                .HasForeignKey<Watcher>(watcher => watcher.userId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}