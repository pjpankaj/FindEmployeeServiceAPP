using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FindEmployeeAPI.DAL
{
    public partial class DemoUserInfoDBContext : DbContext
    {
        public DemoUserInfoDBContext()
        {
        }

        public DemoUserInfoDBContext(DbContextOptions<DemoUserInfoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("Qualification");

                entity.Property(e => e.Qualif)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("qualif");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("UserProfile");

                entity.Property(e => e.CurrentCompany)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Current_Company");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ImageName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PreferredLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Preferred_Location");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.QualificationId)
                    .HasConstraintName("FK_UserProfile_Qualification");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
