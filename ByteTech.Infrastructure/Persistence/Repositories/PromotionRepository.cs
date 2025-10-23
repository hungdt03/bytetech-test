using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using MongoDB.Driver;

namespace ByteTech.Infrastructure.Persistence.Repositories;

public class PromotionRepository(MongoDbContext context) : IPromotionRepository
{
    private readonly IMongoCollection<Promotion> _collection = context.Promotions;

    public async Task<Promotion?> GetByCodeAsync(string code)
    {
        return await _collection.Find(p => p.Code == code && p.IsActive).FirstOrDefaultAsync();
    }

    public async Task<List<Promotion>> GetAllActiveAsync()
    {
        return await _collection.Find(p => p.IsActive).ToListAsync();
    }

    public async Task CreateAsync(Promotion promotion)
    {
        await _collection.InsertOneAsync(promotion);
    }

    public async Task UpdateAsync(Promotion promotion)
    {
        await _collection.ReplaceOneAsync(p => p.Id == promotion.Id, promotion);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(p => p.Id == id);
    }
}