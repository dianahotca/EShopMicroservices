using Microsoft.AspNetCore.Builder;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DbContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();
        }
    }
}
