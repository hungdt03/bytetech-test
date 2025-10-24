using ByteTech.Application.IRepositories;
using ByteTech.Application.Services.Elasticsearch;
using ByteTech.Application.Services.Jwt;
using ByteTech.Infrastructure.Configurations;
using ByteTech.Infrastructure.Elasticsearch;
using ByteTech.Infrastructure.Jwts;
using ByteTech.Infrastructure.Persistence;
using ByteTech.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ByteTech.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPromotionRepository, PromotionRepository>();
        services.AddScoped<IPromotionUsageRepository, PromotionUsageRepository>();

        services.AddScoped<IUserElasticsearchService, UsersElasticsearchService>();
        services.AddScoped<IPromotionElasticsearchService, PromotionElasticsearchService>();
        services.AddScoped<IPromotionUsageElasticsearchService, PromotionUsageElasticsearchService>();

        services.AddSingleton<IElasticClient>(sp =>
        {
            var settings = new ConnectionSettings(new Uri(configuration["ElasticsearchSettings:Uri"] ?? throw new InvalidOperationException("Missing elasticsearch uri")));
            return new ElasticClient(settings);
        });

        services.AddScoped<IJwtService, JwtService>();

        var jwtSettings = new JwtSettings();
        configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
        services.AddSingleton(jwtSettings);

        var mongoDbSettings = new MongoDbSettings();
        configuration.GetSection(nameof(MongoDbSettings)).Bind(mongoDbSettings);
        services.AddSingleton(mongoDbSettings);

        return services;
    }
}
