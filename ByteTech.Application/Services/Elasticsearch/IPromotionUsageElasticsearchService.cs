using ByteTech.Application.Contracts.Responses;

namespace ByteTech.Application.Services.Elasticsearch;

public interface IPromotionUsageElasticsearchService: IElasticsearchService<PromotionUsageResponse>
{
    Task UpdateUserFullNameAsync(string userId, string fullName);
    Task DeleteByPromotionIdAsync(string promotionId);
    Task<IEnumerable<PromotionUsageResponse>> GetByUserIdAsync(string userId);
}

