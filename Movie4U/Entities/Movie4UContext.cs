using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movie4U.ExtensionMethods;
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
            if (modelBuilder == null)
                throw new ArgumentException("modelBuilder argument is null");

            base.OnModelCreating(modelBuilder);

            // entities:
            modelBuilder.Entity<Watcher>().ToTable("Watchers");
            modelBuilder.Entity<Countries>().ToTable("Countries");
            modelBuilder.Entity<Genres>().ToTable("Genres");
            modelBuilder.Entity<TitleDetails>().ToTable("Title_Details");
            modelBuilder.Entity<TitleCountries>().ToTable("Title_Countries");
            modelBuilder.Entity<TitleGenres>().ToTable("Title_Genres");
            modelBuilder.Entity<TitleImages>().ToTable("Title_Images");
            modelBuilder.Entity<WatcherTitle>().ToTable("Watcher_Titles");


            // composite keys for relational entities:
            modelBuilder.Entity<TitleCountries>().HasKey(tc => new { tc.netflix_id, tc.country_code });
            modelBuilder.Entity<TitleGenres>().HasKey(tg => new { tg.netflix_id, tg.genre_id });
            modelBuilder.Entity<WatcherTitle>().HasKey(wt => new { wt.watcher_name, wt.netflix_id });


            // relationships:
            modelBuilder.Entity<Watcher>()
                .HasOne(watcher => watcher.user)
                .WithOne(user => user.watcher)
                .HasForeignKey<Watcher>(watcher => watcher.userId);

            modelBuilder.Entity<WatcherTitle>()
                .HasOne(wt => wt.watcher)
                .WithMany(watcher => watcher.watcherTitles)
                .HasForeignKey(wt => wt.watcher_name);

            modelBuilder.Entity<WatcherTitle>()
                .HasOne(wt => wt.title)
                .WithMany(title => title.watcherTitles)
                .HasForeignKey(wt => wt.netflix_id);

            modelBuilder.Entity<TitleCountries>()
                .HasOne(tc => tc.Country)
                .WithMany(country => country.titleCountries)
                .HasForeignKey(tc => tc.country_code);

            modelBuilder.Entity<TitleCountries>()
                .HasOne(tc => tc.title)
                .WithMany(title => title.titleCountries)
                .HasForeignKey(tc => tc.netflix_id);

            modelBuilder.Entity<TitleGenres>()
                .HasOne(tg => tg.Genre)
                .WithMany(Genre => Genre.titleGenres)
                .HasForeignKey(tg => tg.genre_id);

            modelBuilder.Entity<TitleGenres>()
                .HasOne(tg => tg.title)
                .WithMany(title => title.titleGenres)
                .HasForeignKey(tg => tg.netflix_id);

            modelBuilder.Entity<TitleImages>()
                .HasOne(ti => ti.title)
                .WithMany(title => title.titleImages)
                .HasForeignKey(ti => ti.netflix_id);


            // conventions (from ExtensionMethods.ContextExtension):
            modelBuilder.DeleteBehaviorConvention(DeleteBehavior.Cascade);

        }
    }

}