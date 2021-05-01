using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class GNBCContext : DbContext
    {
        public GNBCContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Sermon> Sermons {get;set;}
        public DbSet<BibleStudy> BibleStudies {get; set;}
        public DbSet<BlogPostCategory> BlogPostCategories {get; set;}
        public DbSet<BlogPost> BlogPosts {get; set;}
        public DbSet<BlogComment> BlogComments {get; set;} 
        public DbSet<ChurchEvent> ChurchEvents {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<UserFavorite> UserFavorites {get; set;}
        public DbSet<RefreshToken> RefreshTokens {get; set;}
        public DbSet <SermonSeries> SermonSeries {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User entity relationships
            modelBuilder.Entity<User>().HasMany(u => u.BlogPosts).WithOne(b => b.User);
            modelBuilder.Entity<User>().HasOne(u => u.UserFavorite).WithOne(uf => uf.User).HasForeignKey<UserFavorite>(u => u.UserId);
            modelBuilder.Entity<User>().HasMany(u => u.BlogComments).WithOne(bc => bc.User);
            
            //User favorite relationships;
            modelBuilder.Entity<UserFavorite>().HasMany(uf => uf.Sermons).WithMany(s => s.UserFavorites);
            modelBuilder.Entity<UserFavorite>().HasMany(uf => uf.ChurchEvents).WithMany(s => s.UserFavorites);
            modelBuilder.Entity<UserFavorite>().HasMany(uf => uf.BlogPosts).WithMany(s => s.UserFavorites);
            modelBuilder.Entity<UserFavorite>().HasMany(uf => uf.BibleStudies).WithMany(s => s.UserFavorites);

            //Blog post entity relationships
            modelBuilder.Entity<BlogPost>().HasMany(bp => bp.BlogCategories).WithMany(bc => bc.BlogPosts);
            modelBuilder.Entity<BlogPost>().HasMany(bp => bp.BlogPostComments).WithOne(bc => bc.BlogPost);

            //Sermon series entity relationships
            modelBuilder.Entity<SermonSeries>().HasMany(ss => ss.Sermons).WithOne(s => s.SermonSeries).HasForeignKey(s => s.SermonSeriesId);
        }

    }
}