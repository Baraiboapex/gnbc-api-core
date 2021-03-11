using Domain;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.BlogPosts).WithOne(b => b.User);
            modelBuilder.Entity<User>().HasOne(u => u.UserFavorite).WithOne(uf => uf.User).HasForeignKey<UserFavorite>(uf => uf.UserId);
        }

    }
}