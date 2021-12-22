using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OFX.Domain.Entities;

namespace OFX.Data.Mapping
{
    public class StatusMap : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");

            builder.HasKey(pk => pk.Id);

            builder.Property(s => s.Code)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(s => s.Severity)
                   .IsRequired()
                   .HasColumnType("int");
        }
    }
}
