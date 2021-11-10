using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.DevTest.Date.Model;

namespace Portal.DevTest.Date.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<ProductModel>
    {

        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("tb_products");

            builder.HasKey(p => p.Id);

            builder.Property(c => c.IsActive)
               .HasColumnName("isActived")
               .HasColumnType("bit")
               .HasDefaultValue(true);

            builder.Property(c => c.Id)
                .HasColumnName("idProduct")
                .HasColumnType("varchar(40)");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(60)");

            builder.Property(c => c.Description)
                 .IsRequired()
                 .HasColumnName("description")
                 .HasColumnType("text");

            builder.Property(c => c.Price)
                .IsRequired()
                .HasColumnName("price")
                .HasColumnType("float")
                .HasPrecision(2,7);

            builder.Property(c => c.CreationDate)
                  .IsRequired()
                  .HasColumnName("creationDate")
                  .HasColumnType("datetime");

        }
    }
}
