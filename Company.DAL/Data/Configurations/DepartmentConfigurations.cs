namespace Company.DAL.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D =>D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D =>D.Code).HasColumnType("varchar(20)");
            builder.Property(D =>D.CreateOn).HasDefaultValueSql("GETDATE()");
            builder.Property(D =>D.ModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
