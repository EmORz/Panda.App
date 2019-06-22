using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Panda.Domein;

namespace Panda.Data
{
    public class PandaDbContext : IdentityDbContext<PandaUser, PandaUserRole, string>
    {
        //public DbSet<PandaUser> Users { get; set; }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Reciept> Reciepts { get; set; }
        public DbSet<PackageStatus> PackageStatuses { get; set; }

        public PandaDbContext(DbContextOptions<PandaDbContext> options)
            : base()
        {
        }

        public PandaDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PandaUser>()
                .HasKey(user => user.Id);

            builder.Entity<PandaUser>()
                .HasMany(user => user.Packages)
                .WithOne(package => package.Recipient)
                .HasForeignKey(reciept => reciept.RecipientId);

            builder.Entity<PandaUser>()
                .HasMany(user => user.Reciepts)
                .WithOne(package => package.Recipient)
                .HasForeignKey(reciept => reciept.RecipientId);

            builder.Entity<Reciept>()
                .HasOne(package => package.Package)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
