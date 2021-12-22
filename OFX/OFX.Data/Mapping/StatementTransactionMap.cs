using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OFX.Domain.Entities;

namespace OFX.Data.Mapping
{
    public class StatementTransactionMap : IEntityTypeConfiguration<StatementTransaction>
    {
        public void Configure(EntityTypeBuilder<StatementTransaction> builder)
        {
            builder.ToTable("StatementTransaction");

            builder.HasKey(pk => pk.Id);

            builder.Property(t => t.TransactionType)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(st => st.Posted)
                   .IsRequired();

            builder.Property(st => st.Amount)
                   .IsRequired()
                   .HasPrecision(13, 2);

            builder.HasOne(st => st.Transaction)
                   .WithMany(t => t.Statements)
                   .IsRequired();

        }
    }
}
