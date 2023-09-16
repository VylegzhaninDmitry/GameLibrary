using System.Text.Json;

namespace GameLibrary;

public static class AppDbContextInitializer
{
    public static void DatabaseInitialize(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppContext>());
    }
    
    private static void SeedData(AppContext? context)
    {
        if (!context!.Set<Genre>().Any())
        {
            var genresData = File.ReadAllText("../GameLibrary/genres.json");
            var genres = JsonSerializer.Deserialize<List<Genre>>(genresData);

            if (genres is not null)
            {
                foreach (var item in genres)
                    context.Set<Genre>().Add(item);
                
                context.SaveChangesAsync();
            }
        }
    }
}