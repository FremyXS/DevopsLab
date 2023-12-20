namespace Laborotor.Services.Rabbit
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRabbit(this IServiceCollection services)
        {
            services.AddScoped<IRabbitMqService, RabbitMqService>();

            return services;
        }
    }
}
