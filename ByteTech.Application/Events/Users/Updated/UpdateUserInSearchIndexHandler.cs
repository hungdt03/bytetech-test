using ByteTech.Application.Mappers;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Events.Users.Updated;

public class UpdateUserInSearchIndexHandler(IUserElasticsearchService elasticsearchService) : INotificationHandler<UserUpdatedEvent>
{
    private readonly IUserElasticsearchService _elasticsearchService = elasticsearchService;
    private readonly UserMapper Mapper = new();

    public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var payload = Mapper.ToResponse(notification.User);
        await _elasticsearchService.UpdateDocumentAsync(payload);
    }
}

