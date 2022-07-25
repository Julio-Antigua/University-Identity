using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityProject.Domain.Entities;

namespace UniversityProject.Infrastructure.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.HasKey(e => e.Id)
                   .HasName("PK__Student__61B35104BDE33261");

            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("IdStudent");

            entity.HasIndex(e => e.Email, "UQ__Student__A9D105342136A528")
                .IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        }
    }
}
