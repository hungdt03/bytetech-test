using ByteTech.Domain.Entities;

namespace ByteTech.Application.IRepositories;

public interface IPromotionRepository : IRepository<Promotion>
{
    Task<Promotion?> GetByCodeAsync(string code);
    Task<List<Promotion>> GetAllActiveAsync();
}
