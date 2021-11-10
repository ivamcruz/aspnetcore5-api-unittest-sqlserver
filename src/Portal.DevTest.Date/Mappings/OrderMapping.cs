using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.DevTest.Date.Model;

namespace Portal.DevTest.Date.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.ToTable("tb_orders");

            builder.HasKey(p => p.Id);

            builder.Property(c => c.IsActive)
                  .HasColumnName("isActived")
                  .HasColumnType("bit")
                  .HasDefaultValue(true);

            builder.Property(c => c.Id)
                .HasColumnName("idOrder")
                .HasColumnType("varchar(40)");

            builder.Property(c => c.CreationDate)
                .HasColumnName("creationDate")
                .HasColumnType("datetime");

            builder.Property(c => c.UserId)
                  .HasColumnName("idUser")
                  .HasColumnType("varchar(40)");

            builder.HasOne(x => x.User)
                .WithMany(c => c.lstOrders)
                .HasForeignKey(d => d.UserId);
        }
    }
}
