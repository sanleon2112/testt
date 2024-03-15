using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using webapi.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace webapi.Core.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Proposal> Proposals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasOne(project => project.Client)
                .WithMany(client => client.Projects)
                .HasForeignKey(project => project.ClientId);

            modelBuilder.Entity<Proposal>()
                .HasOne(proposal => proposal.Project)
                .WithMany(project => project.Proposals)
                .HasForeignKey(proposal => proposal.ProjectId);

            modelBuilder.Entity<Client>()
                .Property(client => client.Size)
                .HasConversion<string>();

            modelBuilder.Entity<Project>()    
                .Property(project => project.Type)
                .HasConversion<string>();
        }
    }
}
