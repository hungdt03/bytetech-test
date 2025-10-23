using ByteTech.Domain.Entities;

namespace ByteTech.Application.IRepositories;

public interface IPromotionUsageRepository
{
    Task CreateAsync(PromotionUsage usage);
    Task<int> CountByUserAsync(string userId, string promotionId);
    Task<List<PromotionUsage>> GetByUserAsync(string userId);
}
