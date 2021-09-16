using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Models
{
    public class GameContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-A8PDHDV\TRAININGSERVER;Initial Catalog=GameDb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordsForUser>()
                .HasKey(c => new { c.UserPhone, c.Word });

            modelBuilder.Entity<User>(entity => entity.Property(e => e.Phone).ValueGeneratedNever());

        }

        public DbSet<User> Users { get; set; }
        public DbSet<WordsForUser> WordsForUsers { get; set; }
    }
}
