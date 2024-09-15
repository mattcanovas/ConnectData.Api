using ConnectData.Api.Data.Contexts;
using System.Data.Entity;

namespace ConnectData.Api.Data.Extensionsl;
public static class DbContextExtensions
{
    public static DbSet GetDbSet(this ConnectDataContext context, string entityTypeName)
    {
        var contextType = context.GetType();
        var dbSetProperty = contextType.GetProperties().FirstOrDefault(
                p => p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                p.PropertyType.GetGenericArguments().First().Name == entityTypeName
            ) ?? throw new InvalidOperationException($"DbSet<{entityTypeName}> não encontrado no context atual.");
        return (DbSet)dbSetProperty.GetValue(context);
    }
}
