using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BasicDatanManagement.Domain.OrganizationAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDatanManagement.Domain.ApprovalAutorityAgg;

namespace BasicDatanManagement.Infrastructure.EFCore.Mapping
{
    public class ApprovalAuthorityMapping : IEntityTypeConfiguration<ApprovalAuthority>
        {
            public void Configure(EntityTypeBuilder<ApprovalAuthority> builder)
            {
                builder.ToTable("ApprovalAuthorities");
                builder.HasKey(x => x.Id);

                builder.Property(x => x.NameEn).HasMaxLength(255).IsRequired();
                builder.Property(x => x.NameFa).HasMaxLength(255);
                builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
                builder.Property(x => x.Address).HasMaxLength(700);
                builder.Property(x => x.WebSite).HasMaxLength(255);
                builder.Property(x => x.Remark).HasMaxLength(1500);
                builder.Property(x => x.Code).HasMaxLength(255);
                builder.Property(x => x.LogoPicture).HasMaxLength(1000);
                builder.Property(x => x.LogoPictureAlt).HasMaxLength(255);
                builder.Property(x => x.LogoPictureTitle).HasMaxLength(500);
                builder.Property(x => x.MetaDescription).HasMaxLength(150);
                builder.Property(x => x.Slug).HasMaxLength(500);
                builder.Property(x => x.CanonicalAddress).HasMaxLength(255);
                builder.Property(x => x.Keywords).HasMaxLength(255);

            }

        }
    }
