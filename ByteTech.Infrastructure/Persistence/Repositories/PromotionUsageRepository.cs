using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using MongoDB.Driver;

namespace ByteTech.Infrastructure.Persistence.Repositories;

public class PromotionUsageRepository(MongoDbContext context) : IPromotionUsageRepository
{
    private readonly IMongoCollection<PromotionUsage> _collection = context.PromotionUsages;

    public async Task<PromotionUsage> AddAsync(PromotionUsage entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(PromotionUsage entity)
    {
        await _collection.DeleteOneAsync(p => p.Id == entity.Id);
    }

    public async Task<IEnumerable<PromotionUsage>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<PromotionUsage> GetByIdAsync(string id)
    {
        return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(PromotionUsage entity)
    {
        var filter = Builders<PromotionUsage>.Filter.Eq(x => x.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteByPromotionIdAsync(string promotionId)
    {
        var filter = Builders<PromotionUsage>.Filter.Eq(x => x.PromotionId, promotionId);
        await _collection.DeleteManyAsync(filter);
    }

    public async Task<int> CountByUserAndPromotionAsync(string userId, string promotionId)
    {
        var filter = Builders<PromotionUsage>.Filter.Eq(x => x.UserId, userId) &
                     Builders<PromotionUsage>.Filter.Eq(x => x.PromotionId, promotionId);

        return (int)await _collection.CountDocumentsAsync(filter);
    }
}
