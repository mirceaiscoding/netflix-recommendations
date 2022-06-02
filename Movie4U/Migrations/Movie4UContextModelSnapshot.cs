﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movie4U.EntitiesModels.Entities;

namespace Movie4U.Migrations
{
    [DbContext(typeof(Movie4UContext))]
    partial class Movie4UContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Country", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("countrycode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("expiring")
                        .HasColumnType("int");

                    b.Property<int>("nl7")
                        .HasColumnType("int");

                    b.Property<int>("tmovs")
                        .HasColumnType("int");

                    b.Property<int>("tseries")
                        .HasColumnType("int");

                    b.Property<int>("tvids")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Genre", b =>
                {
                    b.Property<int>("genre_id")
                        .HasColumnType("int");

                    b.Property<string>("genre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("genre_id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Title", b =>
                {
                    b.Property<string>("netflix_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("alt_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("alt_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("alt_metascore")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("alt_plot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("alt_runtime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("alt_votes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("awards")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("default_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("large_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("latest_date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("maturity_label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("maturity_level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("origin_country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("runtime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("start_date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("synopsis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("netflix_id");

                    b.ToTable("Title_Details");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.TitleCountry", b =>
                {
                    b.Property<string>("netflix_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("country_id")
                        .HasColumnType("int");

                    b.HasKey("netflix_id", "country_id");

                    b.HasIndex("country_id");

                    b.ToTable("Title_Countries");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.TitleGenre", b =>
                {
                    b.Property<string>("netflix_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("genre_id")
                        .HasColumnType("int");

                    b.HasKey("netflix_id", "genre_id");

                    b.HasIndex("genre_id");

                    b.ToTable("Title_Genres");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.TitleImage", b =>
                {
                    b.Property<string>("url")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("image_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("netflix_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("url");

                    b.HasIndex("netflix_id");

                    b.ToTable("Title_Images");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Watcher", b =>
                {
                    b.Property<string>("watcher_name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("refreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("refreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("register_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("watcher_name");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Watchers");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.WatcherGenre", b =>
                {
                    b.Property<string>("watcher_name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("genre_id")
                        .HasColumnType("int");

                    b.Property<double>("watcherGenreScore")
                        .HasColumnType("float");

                    b.HasKey("watcher_name", "genre_id");

                    b.HasIndex("genre_id");

                    b.ToTable("Watcher_Genres");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.WatcherTitle", b =>
                {
                    b.Property<string>("watcher_name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("netflix_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("prefLastSetTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("prefference")
                        .HasColumnType("int");

                    b.Property<bool>("watchLater")
                        .HasColumnType("bit");

                    b.Property<DateTime>("watchLaterLastSetTime")
                        .HasColumnType("datetime2");

                    b.HasKey("watcher_name", "netflix_id");

                    b.HasIndex("netflix_id");

                    b.ToTable("Watcher_Titles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.TitleCountry", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.Country", "Country")
                        .WithMany("titleCountries")
                        .HasForeignKey("country_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movie4U.EntitiesModels.Entities.Title", "title")
                        .WithMany("titleCountries")
                        .HasForeignKey("netflix_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("title");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.TitleGenre", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.Genre", "Genre")
                        .WithMany("titleGenres")
                        .HasForeignKey("genre_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movie4U.EntitiesModels.Entities.Title", "title")
                        .WithMany("titleGenres")
                        .HasForeignKey("netflix_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("title");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.TitleImage", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.Title", "title")
                        .WithMany("titleImages")
                        .HasForeignKey("netflix_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("title");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.UserRole", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movie4U.EntitiesModels.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Watcher", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.User", "user")
                        .WithOne("watcher")
                        .HasForeignKey("Movie4U.EntitiesModels.Entities.Watcher", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.WatcherGenre", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.Genre", "genre")
                        .WithMany("watcherGenres")
                        .HasForeignKey("genre_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movie4U.EntitiesModels.Entities.Watcher", "watcher")
                        .WithMany("watcherGenres")
                        .HasForeignKey("watcher_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("genre");

                    b.Navigation("watcher");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.WatcherTitle", b =>
                {
                    b.HasOne("Movie4U.EntitiesModels.Entities.Title", "title")
                        .WithMany("watcherTitles")
                        .HasForeignKey("netflix_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movie4U.EntitiesModels.Entities.Watcher", "watcher")
                        .WithMany("watcherTitles")
                        .HasForeignKey("watcher_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("title");

                    b.Navigation("watcher");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Country", b =>
                {
                    b.Navigation("titleCountries");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Genre", b =>
                {
                    b.Navigation("titleGenres");

                    b.Navigation("watcherGenres");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Title", b =>
                {
                    b.Navigation("titleCountries");

                    b.Navigation("titleGenres");

                    b.Navigation("titleImages");

                    b.Navigation("watcherTitles");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.User", b =>
                {
                    b.Navigation("watcher");
                });

            modelBuilder.Entity("Movie4U.EntitiesModels.Entities.Watcher", b =>
                {
                    b.Navigation("watcherGenres");

                    b.Navigation("watcherTitles");
                });
#pragma warning restore 612, 618
        }
    }
}
