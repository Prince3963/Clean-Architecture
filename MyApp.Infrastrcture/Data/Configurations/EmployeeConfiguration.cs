using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Domain.Entities;

namespace MyApp.Infrastrcture.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName).IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email).IsRequired();
            builder.HasIndex(e=> e.Email).IsUnique();

            builder.Property(e => e.Password).IsRequired();

            builder.Property(e => e.Salary).IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.ToTable("Employees", e => e.HasCheckConstraint("CK_Employees_Salary", "[Salary] > 0"));

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
