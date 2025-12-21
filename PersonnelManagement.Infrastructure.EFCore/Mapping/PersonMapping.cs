using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelManagement.Domain.PersonAgg;

namespace PersonnelManagement.Infrastructure.EFCore.Mapping
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NameEn).HasMaxLength(255).IsRequired();
            builder.Property(x => x.NameFa).HasMaxLength(255);
            builder.Property(x => x.FamilyEn).HasMaxLength(255).IsRequired();
            builder.Property(x => x.FamilyFa).HasMaxLength(255);
            builder.Property(x => x.FatherName).HasMaxLength(255);
            //builder.Property(x => x.Gender).IsRequired();
            //builder.Property(x => x.Birthday).HasMaxLength(255).IsRequired(false);
            //builder.Property(x => x.Marriage).IsRequired();
            builder.Property(x => x.NationalCode).HasMaxLength(55);
            builder.Property(x => x.PassportNo).HasMaxLength(55);
            builder.Property(x => x.NationalCodeOfFather).HasMaxLength(55);
            //builder.Property(x => x.BirthCityId).HasMaxLength(255).IsRequired(false);
            //builder.Property(x => x.CityId).HasMaxLength(255);
            builder.Property(x => x.Attachment).HasMaxLength(1024);
            builder.Property(x => x.EducationalDegree).HasMaxLength(255);
            builder.Property(x => x.EducationalField).HasMaxLength(255);
            builder.Property(x => x.IDCartNo).HasMaxLength(255);
            //builder.Property(x => x.AddressId).HasMaxLength(255);
            builder.Property(x => x.FatherName).HasMaxLength(255);
            builder.Property(x => x.MobileNo1).HasMaxLength(255); 
            builder.Property(x => x.MobileNo2).HasMaxLength(255);
            builder.Property(x => x.PhoneNo).HasMaxLength(255);
            builder.Property(x => x.MailBoxAddress1).HasMaxLength(255);
            builder.Property(x => x.MailBoxAddress2).HasMaxLength(255);
            builder.Property(x => x.SocialAddress1).HasMaxLength(255);
            builder.Property(x => x.SocialAddress2).HasMaxLength(255);
            //builder.Property(x => x.WorkingStartDate).HasMaxLength(255);
            builder.Property(x => x.ProfilePicture).HasMaxLength(1000);

            //builder.Property(x => x.Activate);


            builder.HasOne(x => x.Group)
                .WithMany(x => x.Persons)
                .HasForeignKey(x => x.PersonGroupId);

            builder.HasMany(x => x.PersonPictures)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);





        }
    }
}
