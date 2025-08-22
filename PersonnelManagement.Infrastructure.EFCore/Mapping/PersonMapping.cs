//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OrganizationManagement.Domain.PersonAgg;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OrganizationManagement.Infrastructure.EFCore.Mapping
//{
//    public class PersonMapping : IEntityTypeConfiguration<Person>
//    {
//        public void Configure(EntityTypeBuilder<Person> builder)
//        {
//            builder.ToTable("Persons");
//            builder.HasKey(x => x.Id);

//            builder.Property(x => x.NameEn).HasMaxLength(255).IsRequired();
//            builder.Property(x => x.NameFa).HasMaxLength(255);
//            builder.Property(x => x.FamilyEn).HasMaxLength(255).IsRequired();
//            builder.Property(x => x.FamilyFa).HasMaxLength(255);
//            builder.Property(x => x.FatherName).HasMaxLength(255);
//            builder.Property(x => x.Birthday).HasMaxLength(255);
//            builder.Property(x => x.NationalCode).HasMaxLength(55);
//            builder.Property(x => x.PassportNo).HasMaxLength(55);
//            builder.Property(x => x.Attachment).HasMaxLength(1000);
//            builder.Property(x => x.Picture).HasMaxLength(1000);
//            builder.Property(x => x.PictureAlt).HasMaxLength(1000);
//            builder.Property(x => x.IDCartNo).HasMaxLength(255);
//            builder.Property(x => x.Address).HasMaxLength(1000);
//            builder.Property(x => x.FatherName).HasMaxLength(255);

//            builder.HasOne(x => x.Organization)
//                .WithMany(x => x.Persons)
//                .HasForeignKey(x => x.OrganizationId);





//        }
//    }
//}
