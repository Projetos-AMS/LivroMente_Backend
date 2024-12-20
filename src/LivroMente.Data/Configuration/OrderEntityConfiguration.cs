using LivroMente.Domain.Models.OrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivroMente.Data.Configuration
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.Property<string>("Id")
                   .ValueGeneratedOnAdd();

            builder.Property(o => o.Date)
                   .IsRequired();

            builder.Property(o => o.ValueTotal)
                   .IsRequired();

            builder.HasOne(u => u.User)
            .WithMany()
            .IsRequired()
            .HasForeignKey(_ => _.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_User");
        }
    }
}