using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Services.Elasticsearch;
using Nest;

namespace ByteTech.Infrastructure.Elasticsearch;

public class PromotionUsageElasticsearchService(IElasticClient elasticClient) : IPromotionUsageElasticsearchService
{
    private readonly IElasticClient _client = elasticClient;
    private readonly string _indexName = "promotionusages";

    public async Task IndexDocumentAsync(PromotionUsageResponse document)
    {
        await _client.IndexAsync(document, idx => idx.Index(_indexName));
    }

    public async Task<PromotionUsageResponse> GetDocumentAsync(string id)
    {
        var response = await _client.GetAsync<PromotionUsageResponse>(id, g => g.Index(_indexName));
        return response.Source;
    }

    public async Task<IEnumerable<PromotionUsageResponse>> SearchAsync(string? searchTerm)
    {
        var response = await _client.SearchAsync<PromotionUsageResponse>(s => s
            .Index(_indexName)
            .Query(q =>
                string.IsNullOrWhiteSpace(searchTerm)
                    ? q.MatchAll()
                    : q.MultiMatch(mm => mm
                        .Fields(f => f
                            .Field(ff => ff.FullName)
                            .Field(ff => ff.UserId)
                            .Field(ff => ff.PromotionId))
                        .Query(searchTerm)
                    )
            )
        );

        return response.Documents;
    }

    public async Task UpdateDocumentAsync(PromotionUsageResponse document)
    {
        await _client.UpdateAsync<PromotionUsageResponse>(document, u => u.Index(_indexName).Doc(document));
    }

    public async Task DeleteDocumentAsync(string id)
    {
        await _client.DeleteAsync<PromotionResponse>(id, d => d.Index(_indexName));
    }

    public async Task UpdateUserFullNameAsync(string userId, string fullName)
    {
        var searchResponse = await _client.SearchAsync<PromotionUsageResponse>(s => s
            .Index(_indexName)
            .Query(q => q.Term(t => t.Field(f => f.UserId).Value(userId)))
        );

        if (searchResponse.Documents.Count == 0)
            return;

        foreach (var doc in searchResponse.Documents)
        {
            var updatedDoc = doc with { FullName = fullName };

            await _client.UpdateAsync<PromotionUsageResponse>(doc.Id, u => u
                .Index(_indexName)
                .Doc(updatedDoc)
            );
        }
    }

    public async Task DeleteByPromotionIdAsync(string promotionId)
    {
        await _client.DeleteByQueryAsync<PromotionUsageResponse>(d => d
            .Index(_indexName)
            .Query(q => q.Term(t => t.Field(f => f.PromotionId).Value(promotionId)))
        );
    }

    public async Task<IEnumerable<PromotionUsageResponse>> GetByUserIdAsync(string userId)
    {
        var response = await _client.SearchAsync<PromotionUsageResponse>(s => s
            .Index(_indexName)
            .Query(q => q.Term(t => t.Field(f => f.UserId).Value(userId)))
        );

        return response.Documents;
    }
}