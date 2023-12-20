using Laborotor.Services.LinksService.Common;

namespace Laborotor.Services.LinksService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLinksService(this IServiceCollection services)
        {
            services.AddTransient<ILinksService, LinksService>();

            return services;
        }
    }
}
