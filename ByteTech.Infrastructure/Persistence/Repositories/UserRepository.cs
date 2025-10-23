using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using MongoDB.Driver;

namespace ByteTech.Infrastructure.Persistence.Repositories;

public class UserRepository(MongoDbContext context) : IUserRepository
{
    private readonly IMongoCollection<User> _collection = context.Users;

    public async Task<User?> GetById(string id)
    {
        return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<(int totalCount, List<User> data)> GetPagedAsync(int pageIndex, int pageSize, string? keyword)
    {
        var filter = Builders<User>.Filter.Empty;

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            keyword = keyword.Trim();
            filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Regex(u => u.FullName, new MongoDB.Bson.BsonRegularExpression(keyword, "i")),
                Builders<User>.Filter.Regex(u => u.Email, new MongoDB.Bson.BsonRegularExpression(keyword, "i"))
            );
        }

        var totalCount = (int)await _collection.CountDocumentsAsync(filter);

        var data = await _collection
            .Find(filter)
            .SortByDescending(u => u.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return (totalCount, data);
    }

    public async Task UpdateAsync(User user)
    {
        var update = Builders<User>.Update
            .Set(u => u.FullName, user.FullName)
            .Set(u => u.Email, user.Email)
            .Set(u => u.IsLocked, user.IsLocked)
            .Set(u => u.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(u => u.Id == user.Id, update);
    }

    public async Task<User> GetByIdAsync(string id)
    {
        return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<User> AddAsync(User entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(User entity)
    {
        await _collection.DeleteOneAsync(u => u.Id == entity.Id);
    }
}