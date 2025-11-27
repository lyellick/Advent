using Advent.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Advent.Shared.Providers;

public static class AdventServiceProvider
{
    private static readonly Lazy<IServiceProvider> _provider =
        new(() =>
        {
            var services = new ServiceCollection();

            services.AddHttpClient<IAdventService, AdventService>();

            return services.BuildServiceProvider();
        });

    public static T Get<T>() where T : notnull =>
        _provider.Value.GetRequiredService<T>();
}