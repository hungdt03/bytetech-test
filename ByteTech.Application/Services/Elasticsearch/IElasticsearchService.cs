namespace ByteTech.Application.Services.Elasticsearch;

public interface IElasticsearchService<T> where T : class
{
    Task IndexDocumentAsync(T document);
    Task UpdateDocumentAsync(T document);
    Task DeleteDocumentAsync(string id);
    Task<T> GetDocumentAsync(string id);
    Task<IEnumerable<T>> SearchAsync(string? searchTerm);
}