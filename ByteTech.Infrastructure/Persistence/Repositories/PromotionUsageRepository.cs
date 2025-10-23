using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using MongoDB.Driver;

namespace ByteTech.Infrastructure.Persistence.Repositories;

public class PromotionUsageRepository(MongoDbContext context) : IPromotionUsageRepository
{
    private readonly IMongoCollection<PromotionUsage> _collection = context.PromotionUsages;

    public async Task CreateAsync(PromotionUsage usage)
    {
        await _collection.InsertOneAsync(usage);
    }

    public async Task<int> CountByUserAsync(string userId, string promotionId)
    {
        return (int)await _collection.CountDocumentsAsync(u => u.UserId == userId && u.PromotionId == promotionId);
    }

    public async Task<List<PromotionUsage>> GetByUserAsync(string userId)
    {
        return await _collection.Find(u => u.UserId == userId).ToListAsync();
    }
}