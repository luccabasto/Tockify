using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tockify.Domain.Models;

namespace Tockify.Infrastructure.Data.Mapping
{
    public class TaskItemMap : IEntityTypeConfiguration<TaskItemModel>
    {
        public void Configure(EntityTypeBuilder<TaskItemModel> builder)
        {
            builder.ToTable("TaskItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Description)
                .HasMaxLength(1000);
            builder.Property(x => x.CreatedAt)
                .IsRequired();
            builder.Property(x => x.DueDate)
                .IsRequired();
            builder.Property(x => x.Status)
                .IsRequired();
            builder.Property(x => x.IsCompleted)
                .IsRequired();
            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);
        }
    }
}
