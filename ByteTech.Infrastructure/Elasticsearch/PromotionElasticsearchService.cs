using ByteTech.Application.Services.Elasticsearch;

namespace ByteTech.Infrastructure.Elasticsearch;

using ByteTech.Application.Contracts.Responses;
using Nest;

public class PromotionElasticsearchService(IElasticClient client) : IPromotionElasticsearchService
{
    private readonly IElasticClient _client = client;
    private readonly string _indexName = "promotions";

    public async Task IndexDocumentAsync(PromotionResponse document)
    {
        await _client.IndexAsync(document, idx => idx.Index(_indexName));
    }

    public async Task UpdateDocumentAsync(PromotionResponse document)
    {
        await _client.UpdateAsync<PromotionResponse>(document, u => u.Index(_indexName).Doc(document));
    }

    public async Task DeleteDocumentAsync(string id)
    {
        await _client.DeleteAsync<PromotionResponse>(id, d => d.Index(_indexName));
    }

    public async Task<PromotionResponse> GetDocumentAsync(string id)
    {
        var response = await _client.GetAsync<PromotionResponse>(id, g => g.Index(_indexName));
        return response.Source;
    }

    public async Task<IEnumerable<PromotionResponse>> SearchAsync(string? searchTerm)
    {
        ISearchResponse<PromotionResponse> response;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            response = await _client.SearchAsync<PromotionResponse>(s => s
                .Index(_indexName)
                .Query(q => q.MatchAll())
            );
        }
        else
        {
            response = await _client.SearchAsync<PromotionResponse>(s => s
                .Index(_indexName)
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f
                            .Field(p => p.Name)
                            .Field(p => p.Code)
                            .Field(p => p.Description)
                        )
                        .Query(searchTerm)
                        .Fuzziness(Fuzziness.Auto)
                    )
                )
            );
        }

        return response.Documents;
    }

    public async Task<IEnumerable<PromotionResponse>> GetActiveAsync()
    {
        var response = await _client.SearchAsync<PromotionResponse>(s => s
            .Index(_indexName)
            .Query(q => q.Term(t => t.Field(f => f.IsActive).Value(true)))
            .Sort(ss => ss.Descending(p => p.StartDate))
        );

        return response.Documents;
    }
}
