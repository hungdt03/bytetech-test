using ByteTech.Domain.Entities;

namespace ByteTech.Application.IRepositories;

public interface IPromotionRepository
{
    Task<Promotion?> GetByCodeAsync(string code);
    Task<List<Promotion>> GetAllActiveAsync();
    Task CreateAsync(Promotion promotion);
    Task UpdateAsync(Promotion promotion);
    Task DeleteAsync(string id);
}
