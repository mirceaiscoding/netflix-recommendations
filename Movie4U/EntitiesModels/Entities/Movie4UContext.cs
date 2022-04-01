using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos.Storage.Internal;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using Movie4U.ExtensionMethods;
using System;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Movie4U.EntitiesModels.Entities
{
    public class Movie4UContext : IdentityDbContext
        <User>
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public Movie4UContext(DbContextOptions<Movie4UContext> options) : base(options) { }

        // entities
        public DbSet<Watcher> Watchers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleCountry> TitleCountries { get; set; }
        public DbSet<TitleGenre> TitleGenres { get; set; }
        public DbSet<TitleImage> TitleImages { get; set; }
        public DbSet<WatcherTitle> WatcherTitles { get; set; }
        public DbSet<WatcherGenre> WatcherGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentException("modelBuilder argument is null");

            base.OnModelCreating(modelBuilder);

            // entities:
            modelBuilder.HasDefaultContainer("Users");

            modelBuilder.Entity<Watcher>().ToContainer("Watchers");
            modelBuilder.Entity<Country>().ToContainer("Countries");
            modelBuilder.Entity<Genre>().ToContainer("Genres");
            modelBuilder.Entity<Title>().ToContainer("Title_Details");
            modelBuilder.Entity<TitleCountry>().ToContainer("Title_Countries");
            modelBuilder.Entity<TitleGenre>().ToContainer("Title_Genres");
            modelBuilder.Entity<TitleImage>().ToContainer("Title_Images");
            modelBuilder.Entity<WatcherTitle>().ToContainer("Watcher_Titles");
            modelBuilder.Entity<WatcherGenre>().ToContainer("Watcher_Genres");
            
            // auth
            modelBuilder.Entity<IdentityUser>().ToContainer("Users");
            modelBuilder.Entity<User>().ToContainer("Users");
            modelBuilder.Entity<IdentityUserRole<string>>().ToContainer("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToContainer("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToContainer("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToContainer("Roles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToContainer("UserTokens");

            modelBuilder.Entity<IdentityRole>()
                .Property(b => b.ConcurrencyStamp)
                .IsETagConcurrency();
            modelBuilder.Entity<User>()
                .Property(b => b.ConcurrencyStamp)
                .IsETagConcurrency();

            // composite keys for relational entities:
            modelBuilder.Entity<TitleCountry>().HasKey(tc => new { tc.netflix_id, tc.country_code });
            modelBuilder.Entity<TitleGenre>().HasKey(tg => new { tg.netflix_id, tg.genre_id });
            modelBuilder.Entity<WatcherTitle>().HasKey(wt => new { wt.watcher_name, wt.netflix_id });
            modelBuilder.Entity<WatcherGenre>().HasKey(wg => new { wg.watcher_name, wg.genre_id });

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

            modelBuilder.Entity<WatcherGenre>()
                .HasOne(wg => wg.watcher)
                .WithMany(watcher => watcher.watcherGenres)
                .HasForeignKey(wg => wg.watcher_name);

            modelBuilder.Entity<WatcherGenre>()
                .HasOne(wg => wg.genre)
                .WithMany(genre => genre.watcherGenres)
                .HasForeignKey(wg => wg.genre_id);

            modelBuilder.Entity<TitleCountry>()
                .HasOne(tc => tc.Country)
                .WithMany(country => country.titleCountries)
                .HasForeignKey(tc => tc.country_code);

            modelBuilder.Entity<TitleCountry>()
                .HasOne(tc => tc.title)
                .WithMany(title => title.titleCountries)
                .HasForeignKey(tc => tc.netflix_id);

            modelBuilder.Entity<TitleGenre>()
                .HasOne(tg => tg.Genre)
                .WithMany(Genre => Genre.titleGenres)
                .HasForeignKey(tg => tg.genre_id);

            modelBuilder.Entity<TitleGenre>()
                .HasOne(tg => tg.title)
                .WithMany(title => title.titleGenres)
                .HasForeignKey(tg => tg.netflix_id);

            modelBuilder.Entity<TitleImage>()
                .HasOne(ti => ti.title)
                .WithMany(title => title.titleImages)
                .HasForeignKey(ti => ti.netflix_id);


            // conventions (from ExtensionMethods.ContextExtension):
            modelBuilder.DeleteBehaviorConvention(DeleteBehavior.Cascade);

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseCosmos(
        //        "AccountEndpoint=...
        //        databaseName: "MoviesDB",
        //        options =>
        //        {
        //            options.ConnectionMode(ConnectionMode.Gateway);
        //            options.WebProxy(new WebProxy());
        //            options.LimitToEndpoint();
        //            options.Region(Regions.NorthEurope);
        //            options.GatewayModeMaxConnectionLimit(32);
        //            options.MaxRequestsPerTcpConnection(8);
        //            options.MaxTcpConnectionsPerEndpoint(16);
        //            options.IdleTcpConnectionTimeout(TimeSpan.FromMinutes(1));
        //            options.OpenTcpConnectionTimeout(TimeSpan.FromMinutes(1));
        //            options.RequestTimeout(TimeSpan.FromMinutes(1));
        //        });
    }
}