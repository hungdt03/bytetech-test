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

    public async Task UpdateAsync(User user)
    {
        await _collection.ReplaceOneAsync(p => p.Id == user.Id, user);
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

    public User? GetByIdSync(string id)
    {
         return _collection.Find(u => u.Id == id).FirstOrDefault();
    }
}