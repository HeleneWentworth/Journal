using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Journal.Models;
using Microsoft.IdentityModel.Tokens;
using Journal.Models.Configuration;

namespace Journal.Data
{
    public class JournalContext : IdentityDbContext<User>
    {
        public JournalContext(DbContextOptions<JournalContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<JournalModel>()
               .HasOne(t => t.Owner)
               .WithMany(o => o.JournalModel)
               .HasForeignKey(t => t.OwnerId);

            modelBuilder.Entity<UserJournal>()
                .HasKey(ut => new { ut.JournalId, ut.UserId });

            modelBuilder.Entity<UserJournal>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserJournalModel)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserJournal>()
                .HasOne(ut => ut.Journal)
                .WithMany(t => t.UserJournal)
                .HasForeignKey(ut => ut.JournalId);
        }


    public DbSet<User> User { get; set; }
        public DbSet<Journal.Models.JournalModel>Journal { get; set; }
        public DbSet<Journal.Models.UserRegistrationModel> UserRegistrationModelsJournal { get; set; }
        




    }
}


