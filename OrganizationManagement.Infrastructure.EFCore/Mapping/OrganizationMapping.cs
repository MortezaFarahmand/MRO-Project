using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationManagement.Domain.OrganizationAgg;


namespace OrganizationManagement.Infrastructure.EFCore.Mapping
{
    public class OrganizationMapping : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NameEn).HasMaxLength(255).IsRequired();
            builder.Property(x => x.NameFa).HasMaxLength(255);
            builder.Property(x => x.DescriptionEn).HasMaxLength(500).IsRequired();
            builder.Property(x => x.DescriptionFa).HasMaxLength(500);
            builder.Property(x => x.AddressEn).HasMaxLength(700);
            builder.Property(x => x.AddressFa).HasMaxLength(700);
            builder.Property(x => x.Tel1).HasMaxLength(50);
            builder.Property(x => x.Tel2).HasMaxLength(50);
            builder.Property(x => x.Fax).HasMaxLength(50);
            builder.Property(x => x.WebSite).HasMaxLength(255);
            builder.Property(x => x.SocialAddress1).HasMaxLength(255);
            builder.Property(x => x.SocialAddress2).HasMaxLength(255);
            builder.Property(x => x.MailBox).HasMaxLength(255);
            builder.Property(x => x.Note).HasMaxLength(1000);
            builder.Property(x => x.Remark).HasMaxLength(1500);
            builder.Property(x => x.OrganizationAviationCodeId).HasMaxLength(255);
            builder.Property(x => x.CompanyRegisterNo).HasMaxLength(150);
            builder.Property(x => x.FormedDate).HasMaxLength(150);
            builder.Property(x => x.ParentId).HasMaxLength(255);
            //builder.Property(x => x.IsActive).HasMaxLength(255).IsRequired();
            builder.Property(x => x.LogoPicture).HasMaxLength(1000);
            builder.Property(x => x.LogoPictureAlt).HasMaxLength(255);
            builder.Property(x => x.LogoPictureTitle).HasMaxLength(500);
            builder.Property(x => x.MetaDescription).HasMaxLength(150);
            builder.Property(x => x.Slug).HasMaxLength(500);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(255);
            builder.Property(x => x.Keywords).HasMaxLength(255);
            //builder.Property(x => x.OrganizationGroupId).HasMaxLength(255).IsRequired();

            builder.HasOne(x => x.Group)
                .WithMany(x => x.Organizations)
                .HasForeignKey(x => x.OrganizationGroupId);

            builder.HasMany(x => x.OrganizationPictures)
                .WithOne(x => x.Organization).HasForeignKey(x => x.OrganizationId);

            //builder.HasOne(x => x.Entiti)
            //    .WithMany(x => x.Organizations)
            //    .HasForeignKey(x => x.EntitiId);
            //builder.HasMany(x => x.Persons)
            //    .WithOne(x => x.Organization)
            //    .HasForeignKey(x => x.OrganizationId);
        }


    }
}
