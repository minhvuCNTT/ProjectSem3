using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pro3MVC.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountCourse> AccountCourses { get; set; } = null!;
        public virtual DbSet<AccountSubject> AccountSubjects { get; set; } = null!;
        public virtual DbSet<Batch> Batches { get; set; } = null!;
        public virtual DbSet<BatchSubject> BatchSubjects { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Enquiry> Enquiries { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<FacultySubject> FacultySubjects { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.Username, "UQ__Account__F3DBC5720E2FF004")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Photo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Role)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<AccountCourse>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.CourseId })
                    .HasName("PK__Account___BE53CDB76A3323DD");

                entity.ToTable("Account_Course");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Fee)
                    .HasColumnType("money")
                    .HasColumnName("fee");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountCourses)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_account_id_Account_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.AccountCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AC_course_id_Course_id");
            });

            modelBuilder.Entity<AccountSubject>(entity =>
            {
                entity.ToTable("Account_Subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.Score)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("score");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountSubjects)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_AE_account_id_Account_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.AccountSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_AE_subject_id_Subject_id");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batch");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Batch_course_id_Course_id");
            });

            modelBuilder.Entity<BatchSubject>(entity =>
            {
                entity.ToTable("Batch_Subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BatchId).HasColumnName("batch_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchSubjects)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_BS_batch_id_Batch_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.BatchSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_BS_subject_id_Subject_id");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.Fee)
                    .HasColumnType("money")
                    .HasColumnName("fee");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Enquiry>(entity =>
            {
                entity.ToTable("Enquiry");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("answer");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Question)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("question");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enquiries)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Enquiry_course_id_Course_id");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Photo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<FacultySubject>(entity =>
            {
                entity.ToTable("Faculty_Subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FacultyId).HasColumnName("faculty_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.FacultySubjects)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK_FS_faculty_id_Faculty_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.FacultySubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_FS_subject_id_Subject_id");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Created)
                    .HasColumnType("date")
                    .HasColumnName("created");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Payment)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("payment");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Invoice_account_id_Account_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Invoice_course_id_Course_id");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Filename)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("filename");

                entity.Property(e => e.Link)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("link");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_Material_subject_id_Subject_id");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Sessions).HasColumnName("sessions");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
