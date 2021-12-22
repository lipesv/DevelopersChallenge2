using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OFX.Domain.Entities;

namespace OFX.Data.Mapping
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(pk => pk.Id);

            builder.Property(a => a.BankId)
                   .IsRequired()
                   .HasMaxLength(4);

            builder.Property(a => a.AccountId)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(u => u.AccountType)
                   .IsRequired()
                   .HasColumnType("int");
        }
    }
}
