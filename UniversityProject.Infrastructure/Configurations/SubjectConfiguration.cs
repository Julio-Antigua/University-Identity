using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;

namespace UniversityProject.Infrastructure.Configurations
{
    internal class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> entity)
        {
            entity.HasKey(e => e.Id)
                    .HasName("PK__Subject__BD949FF5683DD580");

            entity.ToTable("Subject");

            entity.Property(e => e.Id).HasColumnName("IdSubject");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.EndTime).HasColumnType("datetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdCourseNavigation)
                .WithMany(p => p.Subjects)
                .HasForeignKey(d => d.IdCourse)
                .HasConstraintName("FK__Subject__IdCours__628FA481");
        }
    }
}
