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
        public virtual DbSet<SubRole> SubRoles { get; set; } = null!;
        public virtual DbSet<SubRoleMatrix> SubRoleMatrices { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=devopsteam-sql.database.windows.net;Initial Catalog=CapstoneDbV2;User ID=admin_devops;Password=Capstone@123!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditScore>(entity =>
            {
                entity.HasOne(d => d.National)
                    .WithMany(p => p.CreditScores)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CreditScore_Holder");
            });

            modelBuilder.Entity<CriminalRecord>(entity =>
            {
                entity.HasOne(d => d.National)
                    .WithMany(p => p.CriminalRecords)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CriminalRecord_Holder");
            });

            modelBuilder.Entity<EducationRecord>(entity =>
            {
                entity.HasOne(d => d.National)
                    .WithMany(p => p.EducationRecords)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EducationRecord_Holder");
            });

            modelBuilder.Entity<EmploymentHistory>(entity =>
            {
                entity.HasOne(d => d.National)
                    .WithMany(p => p.EmploymentHistories)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmploymentHistory_Holder");
            });

            modelBuilder.Entity<IdentityDetail>(entity =>
            {
                entity.HasOne(d => d.National)
                    .WithMany(p => p.IdentityDetails)
                    .HasPrincipalKey(p => p.NationalId)
                    .HasForeignKey(d => d.NationalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdentityDetail_Holder");
            });

            modelBuilder.Entity<Request>(entity =>
            {
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

            modelBuilder.Entity<SubRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.SubRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubRole_Role");
            });

            modelBuilder.Entity<SubRoleMatrix>(entity =>
            {
                entity.HasOne(d => d.SubRole)
                    .WithMany(p => p.SubRoleMatrices)
                    .HasForeignKey(d => d.SubRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubRoleMatrix_SubRole");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.SubRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SubRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_SubRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
