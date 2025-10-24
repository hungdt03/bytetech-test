using ByteTech.Application.Contracts.Responses;

namespace ByteTech.Application.Services.Elasticsearch;

public interface IPromotionElasticsearchService : IElasticsearchService<PromotionResponse>
{
    Task<IEnumerable<PromotionResponse>> GetActiveAsync();
}

