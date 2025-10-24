using ByteTech.Domain.Entities;

namespace ByteTech.Application.IRepositories;

public interface IPromotionUsageRepository : IRepository<PromotionUsage>
{
    Task DeleteByPromotionIdAsync(string promotionId);
    Task<int> CountByUserAndPromotionAsync(string userId, string promotionId);
}
