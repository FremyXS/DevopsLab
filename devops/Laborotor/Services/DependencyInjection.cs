using Laborotor.Services.Http;
using Laborotor.Services.LinksService;
using Laborotor.Services.LinksService.Common;
using Laborotor.Services.Rabbit;

namespace Laborotor.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpService, HttpService>();
            services.AddLinksService();
            services.AddRabbit();

            return services;
        }
    }
}
