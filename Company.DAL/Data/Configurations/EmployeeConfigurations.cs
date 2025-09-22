

using Company.DAL.Models.EmployeeModule;
using Company.DAL.Models.Shared;

namespace Company.DAL.Data.Configurations
{
    public class EmployeeConfigurations : BaseEntityConfigurations<Employee> , IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Address).HasColumnType("varchar(50)");
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E => E.Gender).HasConversion
                            (
                              (empGender) => empGender.ToString(),
                              (gender) =>(Gender) Enum.Parse(typeof(Gender),gender)
                            );
            builder.Property(E => E.EmployeeType).HasConversion
                            (
                              (empType) => empType.ToString(),
                              (empType) => (EmployeeType) Enum.Parse(typeof(EmployeeType), empType)
                            );
            base.Configure(builder);
        }
    }
}
