using Microsoft.EntityFrameworkCore;
using WebApplication5.Service;
using WebApplication5.Domain.Entities;
using WebApplication5.Domain.Entities;

namespace WebApplication5.Data
{
    public class DataBaseContext : DbContext
    {
        public DbSet<City>? Cities { get; set; }
        public DbSet<Office>? Offices { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<MapPoint>? MapPoints { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(DbConfig.DbConnectionString);
        }
    }
}







//using Microsoft.EntityFrameworkCore;
//using WebApplication2.Service;
//using WebApplication2.Domain.Entities;

//namespace WebApplication2.Data
//{
//    public class DataBaseContext : DbContext
//    {
//        public DbSet<User>? Users { get; set; }
//        public DbSet<Office>? Offices { get; set; }
//        public DbSet<Post>? Posts { get; set; }
//        public DbSet<Publication>? Publications { get; set; }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            base.OnConfiguring(optionsBuilder);
//            optionsBuilder.UseSqlServer(DbConfig.DbConnectionString);
//        }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Publication>().HasKey(u => new { u.PostId, u.OfficeId });
//        }
//    }
//}
