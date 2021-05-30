using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.IO;

#nullable disable

namespace ARM_dolg
{
    public partial class DolgContext : DbContext
    {

        static private string connectionString = default;
        static DolgContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            DolgContext.connectionString = config.GetConnectionString("DefaultConnection");
        }
        public DolgContext()
        {
        }

        public DolgContext(DbContextOptions<DolgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GroupTeacher> GroupTeachers { get; set; }
        public virtual DbSet<Lab> Labs { get; set; }
        public virtual DbSet<StudGroup> StudGroups { get; set; }
        public virtual DbSet<StudSubject> StudSubjects { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentLab> StudentLabs { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DolgContext.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<GroupTeacher>(entity =>
            {
                entity.ToTable("GroupTeacher");

                entity.HasIndex(e => new { e.Преподаватель, e.НомерГруппы, e.УчебныйПредмет }, "УникальныйГруппаПреподаватель")
                    .IsUnique();

                entity.HasOne(d => d.НомерГруппыNavigation)
                    .WithMany(p => p.GroupTeachers)
                    .HasForeignKey(d => d.НомерГруппы)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupTeac__Номер__7A672E12");

                entity.HasOne(d => d.ПреподавательNavigation)
                    .WithMany(p => p.GroupTeachers)
                    .HasForeignKey(d => d.Преподаватель)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupTeac__Препо__797309D9");

                entity.HasOne(d => d.УчебныйПредметNavigation)
                    .WithMany(p => p.GroupTeachers)
                    .HasForeignKey(d => d.УчебныйПредмет)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupTeac__Учебн__7B5B524B");
            });

            modelBuilder.Entity<Lab>(entity =>
            {
                entity.ToTable("Lab");

                entity.HasIndex(e => new { e.ГруппаПреподаватель, e.ДатаЗанятия }, "УникальнаяРабота")
                    .IsUnique();

                entity.Property(e => e.ДатаЗанятия).HasColumnType("date");

                entity.HasOne(d => d.ГруппаПреподавательNavigation)
                    .WithMany(p => p.Labs)
                    .HasForeignKey(d => d.ГруппаПреподаватель)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab__ГруппаПрепо__7F2BE32F");
            });

            modelBuilder.Entity<StudGroup>(entity =>
            {
                entity.ToTable("StudGroup");

                entity.HasIndex(e => e.Номер, "UQ__StudGrou__063C4BF704446757")
                    .IsUnique();

                entity.Property(e => e.Номер)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<StudSubject>(entity =>
            {
                entity.ToTable("StudSubject");

                entity.HasIndex(e => e.Название, "UQ__StudSubj__38DA803535D04663")
                    .IsUnique();

                entity.Property(e => e.Название)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => new { e.Фио, e.НомерГруппы }, "УникальныйСтудент")
                    .IsUnique();

                entity.Property(e => e.Пароль)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Фио)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("ФИО");

                entity.HasOne(d => d.НомерГруппыNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.НомерГруппы)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Student__НомерГр__02FC7413");
            });

            modelBuilder.Entity<StudentLab>(entity =>
            {
                entity.ToTable("StudentLab");

                entity.HasIndex(e => new { e.ПрактическияРабота, e.Студент }, "Работа")
                    .IsUnique();

                entity.Property(e => e.Статус)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ПрактическияРаботаNavigation)
                    .WithMany(p => p.StudentLabs)
                    .HasForeignKey(d => d.ПрактическияРабота)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentLa__Практ__06CD04F7");

                entity.HasOne(d => d.СтудентNavigation)
                    .WithMany(p => p.StudentLabs)
                    .HasForeignKey(d => d.Студент)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentLa__Студе__07C12930");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasIndex(e => e.Фио, "UQ__Teacher__A9F75A14D8CEE705")
                    .IsUnique();

                entity.Property(e => e.Пароль)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Фио)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("ФИО");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
