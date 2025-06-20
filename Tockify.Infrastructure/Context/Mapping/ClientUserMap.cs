using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tockify.Domain.Models;

namespace Tockify.Infrastructure.Data.Mapping
{
    public class ClientUserMap : IEntityTypeConfiguration<ClientUserModel>
    {
        public void Configure(EntityTypeBuilder<ClientUserModel> builder)
        {
            builder.ToTable("ClientUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
