using BasicDataManagement.Domain.CountryAgg;
using BasicDataManagement.Infrastructure.EFCore.Repository.DocumentSequence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class DocumentSequenceMapping : IEntityTypeConfiguration<DocumentSequence>
    {
        public void Configure(EntityTypeBuilder<DocumentSequence> builder)
        {
            builder.ToTable("DocumentSequences");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DocumentType).HasMaxLength(20).IsRequired();
            builder.HasIndex(x => new { x.DocumentType, x.Year, x.BaseId }).IsUnique();
        }
    }
}
