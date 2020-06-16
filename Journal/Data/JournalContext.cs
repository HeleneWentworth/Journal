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

        }

       public DbSet<User> User { get; set; }
        public DbSet<Journal.Models.JournalModel>Journal { get; set; }
    }
}


