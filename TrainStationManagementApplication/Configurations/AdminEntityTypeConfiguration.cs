using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainStationManagementApplication.Models.Entities;

namespace TrainStationManagementApplication.Configurations
{
    public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
        //    builder.ToTable("Admins");
        //    builder.Property(s => s.Id)
        //        .HasColumnType("string").IsRequired();
        //    builder.HasIndex(s => s.User.Email).IsUnique();
        //    //builder.HasIndex(s => s.PhoneNumber).IsUnique();
        //    builder.HasMany(s => s.UserId)
        //        .WithOne(s => s.Student)
        //        .HasForeignKey(s => s.StudentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
