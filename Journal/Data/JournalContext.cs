using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Journal.Models;
using Microsoft.IdentityModel.Tokens;

namespace Journal.Models
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

            // modelBuilder.ApplyConfiguration(new RoleConfiguration());

        }

        public DbSet<Journal> Journal { get; set; }
    }
}


