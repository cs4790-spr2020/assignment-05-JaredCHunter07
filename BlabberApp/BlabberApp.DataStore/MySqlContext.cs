using Microsoft.EntityFrameworkCore;
using BlabberApp.Domain;

namespace BlabberApp.DataStore
{
    public class MySqlContext : DbContext
    {
        public DbSet<Blab> Blab { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //you may want to change the ip, it looks unfinished,
            optionsBuilder.UseMySQL("server=142.93.114.73;database=JaredCHunter07;"
            + "user=JaredCHunter07;password=letmein");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.LastLoginDTTM).IsRequired();
                entity.Property(e => e.RegisterDTTM).IsRequired();
            });

            modelBuilder.Entity<Blab>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DTTM).IsRequired();
                entity.Property(e => e.Message);
                entity.HasOne(u => u.User);
            });
        }
    }
}