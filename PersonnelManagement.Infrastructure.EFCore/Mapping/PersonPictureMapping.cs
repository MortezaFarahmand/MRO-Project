using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelManagement.Domain.PersonPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastructure.EFCore.Mapping
{
    public class PersonPictureMapping : IEntityTypeConfiguration<PersonPicture>
    {
        public void Configure(EntityTypeBuilder<PersonPicture> builder)
        {
            builder.ToTable("PersonPictures");
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.Title).HasMaxLength(255);
            builder.Property(x => x.Text).HasMaxLength(500);
            builder.Property(x => x.Remark).HasMaxLength(500);

            builder.HasOne(x => x.Person)
                .WithMany(x => x.PersonPictures)
                .HasForeignKey(x => x.PersonId);

        }
    }
}
