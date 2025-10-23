using ByteTech.Application.Mappers;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Events.Users.Created;

public class UserCreatedEventHandler(IUserElasticsearchService elasticsearchService) : INotificationHandler<UserCreatedEvent>
{
    private readonly IUserElasticsearchService _elasticsearchService = elasticsearchService;
    private readonly UserMapper Mapper = new();

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var payload = Mapper.ToResponse(notification.User);
        await _elasticsearchService.IndexDocumentAsync(payload);
    }
}
