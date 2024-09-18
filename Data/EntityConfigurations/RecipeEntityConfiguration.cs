using LearningASP.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class RecipeEntityConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder
            .HasKey(recipe => recipe.Id);

        builder
            .HasOne(recipe => recipe.User);

        builder
            .Property(recipe => recipe.Title)
            .IsRequired();

        builder
            .Property(recipe => recipe.Description)
            .IsRequired();
    }
}