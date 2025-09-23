using Company.DAL.Models.Shared;

namespace Company.DAL.Data.Configurations
{
    public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(D => D.CreateOn).HasDefaultValueSql("GETDATE()");
            builder.Property(D => D.ModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
