using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.IO;

#nullable disable

namespace ARM_dolg
{
    public partial class ApplicationContext : DbContext
    {
        static private string connectionString = default;
        static ApplicationContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            ApplicationContext.connectionString = config.GetConnectionString("DefaultConnection");
        }
        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public virtual DbSet<Группы> Группы { get; set; }
        public virtual DbSet<ГруппыПреподаватели> ГруппыПреподаватели { get; set; }
        public virtual DbSet<ПрактическиеРаботы> ПрактическиеРаботы { get; set; }
        public virtual DbSet<Преподаватели> Преподаватели { get; set; }
        public virtual DbSet<Студенты> Студентыs { get; set; }
        public virtual DbSet<СтудентыПрактическиеРаботы> СтудентыПрактическиеРаботы { get; set; }
        public virtual DbSet<УчебныеПредметы> УчебныеПредметы { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Группы>(entity =>
            {
                entity.ToTable("Группы");

                entity.Property(e => e.Номер)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<ГруппыПреподаватели>(entity =>
            {
                entity.ToTable("ГруппыПреподаватели");

                entity.HasOne(d => d.НомерГруппыNavigation)
                    .WithMany(p => p.ГруппыПреподаватели)
                    .HasForeignKey(d => d.НомерГруппы)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ГруппыПре__Номер__5535A963");

                entity.HasOne(d => d.ПреподавательNavigation)
                    .WithMany(p => p.ГруппыПреподаватели)
                    .HasForeignKey(d => d.Преподаватель)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ГруппыПре__Препо__5441852A");

                entity.HasOne(d => d.УчебныйПредметNavigation)
                    .WithMany(p => p.ГруппыПреподаватели)
                    .HasForeignKey(d => d.УчебныйПредмет)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ГруппыПре__Учебн__5629CD9C");
            });

            modelBuilder.Entity<ПрактическиеРаботы>(entity =>
            {
                entity.ToTable("ПрактическиеРаботы");

                entity.Property(e => e.ДатаЗанятия).HasColumnType("date");

                entity.HasOne(d => d.ГруппаПреподавательNavigation)
                    .WithMany(p => p.ПрактическиеРаботы)
                    .HasForeignKey(d => d.ГруппаПреподаватель)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Практичес__Групп__59063A47");
            });

            modelBuilder.Entity<Преподаватели>(entity =>
            {
                entity.ToTable("Преподаватели");

                entity.Property(e => e.Пароль)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Фио)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("ФИО");
            });

            modelBuilder.Entity<Студенты>(entity =>
            {
                entity.ToTable("Студенты");

                entity.Property(e => e.Пароль)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Фио)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("ФИО");

                entity.HasOne(d => d.НомерГруппыNavigation)
                    .WithMany(p => p.Студенты)
                    .HasForeignKey(d => d.НомерГруппы)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Студенты__НомерГ__5BE2A6F2");
            });

            modelBuilder.Entity<СтудентыПрактическиеРаботы>(entity =>
            {
                entity.ToTable("СтудентыПрактическиеРаботы");

                entity.Property(e => e.Статус)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ПрактическияРаботаNavigation)
                    .WithMany(p => p.СтудентыПрактическиеРаботы)
                    .HasForeignKey(d => d.ПрактическияРабота)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__СтудентыП__Практ__5EBF139D");

                entity.HasOne(d => d.СтудентNavigation)
                    .WithMany(p => p.СтудентыПрактическиеРаботы)
                    .HasForeignKey(d => d.Студент)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__СтудентыП__Студе__5FB337D6");
            });

            modelBuilder.Entity<УчебныеПредметы>(entity =>
            {
                entity.ToTable("УчебныеПредметы");

                entity.Property(e => e.Название)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
