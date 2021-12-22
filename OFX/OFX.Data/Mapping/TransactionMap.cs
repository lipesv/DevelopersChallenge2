using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OFX.Domain.Entities;

namespace OFX.Data.Mapping
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(pk => pk.Id);

            builder.Property(t => t.Start)
                   .IsRequired();

            builder.Property(t => t.End)
                   .IsRequired();
        }
    }
}
