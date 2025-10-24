using ByteTech.Domain.Entities;

namespace ByteTech.Application.IRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    User? GetByIdSync(string id);
}
