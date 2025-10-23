using ByteTech.Domain.Entities;

namespace ByteTech.Application.IRepositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<(int totalCount, List<User> data)> GetPagedAsync(int pageIndex, int pageSize, string? keyword);
}
