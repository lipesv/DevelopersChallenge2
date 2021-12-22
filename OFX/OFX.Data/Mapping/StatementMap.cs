using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OFX.Domain.Entities;

namespace OFX.Data.Mapping
{
    public class StatementMap : IEntityTypeConfiguration<Statement>
    {
        public void Configure(EntityTypeBuilder<Statement> builder)
        {
            builder.ToTable("Statement");

            builder.HasKey(pk => pk.Id);

            builder.Property(st => st.UId)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(st => st.Currency)
                   .IsRequired()
                   .HasMaxLength(3);

            builder.HasOne(st => st.Status)
                   .WithOne(s => s.Statement)
                   .HasForeignKey<Statement>(st => st.StatusId);

            builder.HasOne(st => st.Account)
                   .WithOne(a => a.Statement)
                   .HasForeignKey<Statement>(a => a.AccountId);

        }
    }
}
