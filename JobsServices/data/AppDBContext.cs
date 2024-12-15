﻿
using JobsServices.models.Entity;
using Microsoft.EntityFrameworkCore;




namespace JobServices.data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        // Table mapping
        public DbSet<Job> Jobs { get; set; }

        // View mapping
        public DbSet<latest_statuses> latest_Statuses { get; set; }
        public DbSet<candidate_applied> candidate_Applieds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly map table name for "Job" (case-sensitive in PostgreSQL)
            modelBuilder.Entity<Job>().ToTable("Job", schema: "ADANI_TALENT");

            modelBuilder.Entity<latest_statuses>()
                .HasNoKey()
                .ToView("V_HM_CANDIDATE_SHORTLISTED", schema: "ADANI_TALENT"); // Replace with your actual view name and schema

            base.OnModelCreating(modelBuilder);
            // Configure view mapping
        
                
            modelBuilder.Entity<candidate_applied>()
                .ToView("V_HM_CANDIDATE_APPLIED", schema: "ADANI_TALENT") // Map view with schema

                .HasNoKey(); // Views generally do not have primary keys
        }
    }
}
