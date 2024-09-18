using LearningASP.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(user => user.Id);

        builder
            .Property(user => user.Email)
            .IsRequired();

        builder
            .Property(user => user.Password)
            .IsRequired();

        builder
            .Property(user => user.FirstName)
            .IsRequired();

        builder
            .Property(user => user.LastName)
            .IsRequired();
    }
}