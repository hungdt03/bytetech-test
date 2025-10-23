using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Services.Elasticsearch;
using Nest;

namespace ByteTech.Infrastructure.Elasticsearch;

public class UsersElasticsearchService(IElasticClient client) : IUserElasticsearchService
{
    private readonly IElasticClient _client = client;
    private readonly string _indexName = "users";

    public async Task IndexDocumentAsync(UserResponse document)
    {
        await _client.IndexAsync(document, idx => idx.Index(_indexName));
    }

    public async Task UpdateDocumentAsync(UserResponse document)
    {
        await _client.UpdateAsync<UserResponse>(document, u => u.Index(_indexName).Doc(document));
    }

    public async Task DeleteDocumentAsync(string id)
    {
        await _client.DeleteAsync<UserResponse>(id, d => d.Index(_indexName));
    }

    public async Task<UserResponse> GetDocumentAsync(string id)
    {
        var response = await _client.GetAsync<UserResponse>(id, g => g.Index(_indexName));
        return response.Source;
    }

    public async Task<IEnumerable<UserResponse>> SearchAsync(string? searchTerm)
    {
        ISearchResponse<UserResponse> response;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            response = await _client.SearchAsync<UserResponse>(s => s
                .Index(_indexName)
                .Query(q => q.MatchAll())
            );
        }
        else
        {
            response = await _client.SearchAsync<UserResponse>(s => s
                .Index(_indexName)
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f
                            .Field(u => u.FullName)
                            .Field(u => u.Email)
                        )
                        .Query(searchTerm)
                        .Fuzziness(Fuzziness.Auto)
                    )
                )
            );
        }

        if (!response.IsValid)
            throw new Exception($"Search failed: {response.OriginalException?.Message}");

        return response.Documents;
    }
}
