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
    internal class DetailsSubjectConfiguration : IEntityTypeConfiguration<DetailsSubject>
    {
        public void Configure(EntityTypeBuilder<DetailsSubject> entity)
        {
    
            entity.HasKey(e => new { e.IdSubject, e.IdStudent })
                    .HasName("PK__Details___EB8FAAE597DC3313");

            entity.ToTable("Details_Subjects");

            entity.HasOne(d => d.IdStudentNavigation)
                .WithMany(p => p.DetailsSubjects)
                .HasForeignKey(d => d.IdStudent)
                .HasConstraintName("FK__Details_S__IdStu__693CA210");

            entity.HasOne(d => d.IdSubjectNavigation)
                .WithMany(p => p.DetailsSubjects)
                .HasForeignKey(d => d.IdSubject)
                .HasConstraintName("FK__Details_S__IdSub__68487DD7");
        }
    }
}
