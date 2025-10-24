using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using MongoDB.Driver;

namespace ByteTech.Infrastructure.Persistence.Repositories;

public class PromotionRepository(MongoDbContext context) : IPromotionRepository
{
    private readonly IMongoCollection<Promotion> _collection = context.Promotions;

    public async Task<Promotion?> GetByCodeAsync(string code)
    {
        return await _collection.Find(p => p.Code == code).FirstOrDefaultAsync();
    }

    public async Task<List<Promotion>> GetAllActiveAsync()
    {
        return await _collection.Find(p => p.IsActive).ToListAsync();
    }

    public async Task UpdateAsync(Promotion promotion)
    {
        await _collection.ReplaceOneAsync(p => p.Id == promotion.Id, promotion);
    }

    public async Task<Promotion> GetByIdAsync(string id)
    {
        return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Promotion>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Promotion> AddAsync(Promotion entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(Promotion entity)
    {
        await _collection.DeleteOneAsync(p => p.Id == entity.Id);
    }
}