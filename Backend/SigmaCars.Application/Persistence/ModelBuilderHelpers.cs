using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SigmaCars.Application.Persistence;

internal static class ModelBuilderHelpers
{
    public static EntityTypeBuilder<T> Entity<T>(
        this ModelBuilder modelBuilder,
        Action<EntityTypeBuilder<T>> action)
        where T : class
    {
        var e = modelBuilder.Entity<T>();
        action(modelBuilder.Entity<T>());
        return e;
    }
}