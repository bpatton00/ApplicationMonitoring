using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationVersionTracking.Models;
using Microsoft.EntityFrameworkCore;


namespace ApplicationVersionTracking.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<EnvironmentType> EnvironmentTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>().ToTable("Application");
            modelBuilder.Entity<EnvironmentType>().ToTable("EnvironmentType");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<EventType>().ToTable("EventType");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Server>().ToTable("Server");
            modelBuilder.Entity<StatusType>().ToTable("StatusType");
        }
    }
}

