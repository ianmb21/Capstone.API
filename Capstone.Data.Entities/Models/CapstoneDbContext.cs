using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Capstone.Data.Entities.Models
{
    public partial class CapstoneDbContext : DbContext
    {
        public CapstoneDbContext()
        {
        }

        public CapstoneDbContext(DbContextOptions<CapstoneDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CreditScore> CreditScores { get; set; } = null!;
        public virtual DbSet<CriminalRecord> CriminalRecords { get; set; } = null!;
        public virtual DbSet<EducationRecord> EducationRecords { get; set; } = null!;
        public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; } = null!;
        public virtual DbSet<Holder> Holders { get; set; } = null!;
        public virtual DbSet<IdentityDetail> IdentityDetails { get; set; } = null!;
        public virtual DbSet<RecordType> RecordTypes { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=devopsteam-sql.database.windows.net;Initial Catalog=CapstoneDb;User ID=admin_devops;Password=Capstone@123!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditScore>(entity =>
            {
                entity.HasKey(e => e.CreditSoreId);

                entity.ToTable("CreditScore");

                entity.Property(e => e.AccountNumber).HasMaxLength(20);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.CreditStatus).HasMaxLength(20);

                entity.Property(e => e.NationalId).HasMaxLength(20);

                entity.Property(e => e.ScoreRange).HasMaxLength(20);

                entity.HasOne(d => d.National)
                    .WithMany(p => p.CreditScores)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditScore_Holder");
            });

            modelBuilder.Entity<CriminalRecord>(entity =>
            {
                entity.ToTable("CriminalRecord");

                entity.Property(e => e.CrimeCommitted).HasMaxLength(100);

                entity.Property(e => e.DateCommitted).HasColumnType("date");

                entity.Property(e => e.NationalId).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.National)
                    .WithMany(p => p.CriminalRecords)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CriminalRecord_Holder");
            });

            modelBuilder.Entity<EducationRecord>(entity =>
            {
                entity.ToTable("EducationRecord");

                entity.Property(e => e.Course).HasMaxLength(100);

                entity.Property(e => e.LevelOfEducation).HasMaxLength(50);

                entity.Property(e => e.NationalId).HasMaxLength(20);

                entity.Property(e => e.SchoolName).HasMaxLength(150);

                entity.HasOne(d => d.National)
                    .WithMany(p => p.EducationRecords)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EducationRecord_Holder");
            });

            modelBuilder.Entity<EmploymentHistory>(entity =>
            {
                entity.ToTable("EmploymentHistory");

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.DateEnded).HasColumnType("date");

                entity.Property(e => e.DateStarted).HasColumnType("date");

                entity.Property(e => e.NationalId).HasMaxLength(20);

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.HasOne(d => d.National)
                    .WithMany(p => p.EmploymentHistories)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmploymentHistory_Holder");
            });

            modelBuilder.Entity<Holder>(entity =>
            {
                entity.ToTable("Holder");

                entity.HasIndex(e => e.NationalId, "UC_NationalId")
                    .IsUnique();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.NationalId).HasMaxLength(20);
            });

            modelBuilder.Entity<IdentityDetail>(entity =>
            {
                entity.ToTable("IdentityDetail");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.NationalId).HasMaxLength(20);

                entity.HasOne(d => d.National)
                    .WithMany(p => p.IdentityDetails)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdentityDetail_Holder");
            });

            modelBuilder.Entity<RecordType>(entity =>
            {
                entity.ToTable("RecordType");

                entity.Property(e => e.RecordName).HasMaxLength(50);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.DateApproved).HasColumnType("datetime");

                entity.Property(e => e.DateIssued).HasColumnType("datetime");

                entity.Property(e => e.DateRequested).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IssuedBy).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.NationalId).HasMaxLength(20);

                entity.Property(e => e.RecordLink).HasMaxLength(200);

                entity.Property(e => e.Remarks).HasMaxLength(200);

                entity.Property(e => e.RequestStatus).HasMaxLength(20);

                entity.Property(e => e.VerifiedBy).HasMaxLength(50);

                entity.HasOne(d => d.National)
                    .WithMany(p => p.Requests)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Holder");

                entity.HasOne(d => d.RecordType)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RecordTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_RecordType");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName).HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
