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
            .Property(user => user.PasswordHash)
            .IsRequired();

        builder
            .Property(user => user.Salt)
            .IsRequired();

        builder
            .Property(user => user.FirstName)
            .IsRequired();

        builder
            .Property(user => user.LastName)
            .IsRequired();

        builder
            .HasMany(user => user.Recipes)
            .WithOne(recipe => recipe.User)
            .HasForeignKey(recipe => recipe.Id)
            .HasPrincipalKey(user => user.Id);
    }
}