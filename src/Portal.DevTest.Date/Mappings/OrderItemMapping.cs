using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.DevTest.Date.Model;

namespace Portal.DevTest.Date.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItemModel>
    {
        public void Configure(EntityTypeBuilder<OrderItemModel> builder)
        {
            builder.ToTable("tb_orderItems");

            builder.HasKey(p => p.Id);

            builder.Property(c => c.IsActive)
                 .HasColumnName("isActived")
                 .HasColumnType("bit")
                 .HasDefaultValue(true);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(40)");

            builder.Property(c => c.CurrentPrice)
                .HasColumnName("currentPrice")
                .HasColumnType("decimal")
                .HasPrecision(6, 5);

            builder.Property(c => c.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal")
                .HasPrecision(6, 2);

            builder.Property(c => c.OrderId)
                .IsRequired()
                .HasColumnName("idOrder")
                .HasColumnType("varchar(40)");

            builder.Property(c => c.ProductId)
                .IsRequired()
                .HasColumnName("idProduct")
                .HasColumnType("varchar(40)");


            builder.HasOne(x => x.Product)
                .WithMany(c => c.lstOrderItems)
                .HasForeignKey(d => d.OrderId);

            builder.HasOne(x => x.Order)
                .WithMany(c => c.lstOrderItems)
                .HasForeignKey(d => d.OrderId);

        }
    }
}
