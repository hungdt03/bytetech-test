using ByteTech.Domain.Entities;
using ByteTech.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ByteTech.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Promotion> Promotions => _database.GetCollection<Promotion>("Promotions");
    public IMongoCollection<PromotionUsage> PromotionUsages => _database.GetCollection<PromotionUsage>("PromotionUsages");
}