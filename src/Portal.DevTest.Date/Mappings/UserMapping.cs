using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.DevTest.Date.Model;

namespace Portal.DevTest.Date.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("tb_users");

            builder.HasKey(p => p.Id);

            builder.Property(c => c.IsActive)
                .HasColumnName("isActived")
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(c => c.Id)
                .HasColumnName("idUser")
                .HasColumnType("varchar(40)");

            builder.Property(c => c.DisplayName)
                .IsRequired()
                .HasColumnName("displayName")
                .HasColumnType("varchar(60)");

            builder.Property(c => c.Email)
               .IsRequired()
               .HasColumnName("email")
               .HasColumnType("varchar(100)");

            builder.Property(c => c.UserName)
                .IsRequired()
                .HasColumnName("userName")
                .HasColumnType("varchar(50)");

            builder.Property(c => c.CreationDate)
               .IsRequired()
               .HasColumnName("creationDate")
               .HasColumnType("datetime");

            builder.Property(c => c.Password)
               .IsRequired()
               .HasColumnName("password")
               .HasColumnType("varchar(50)");
        }
    }
}
