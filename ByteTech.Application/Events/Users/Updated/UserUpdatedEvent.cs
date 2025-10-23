using ByteTech.Domain.Entities;
using MediatR;

namespace ByteTech.Application.Events.Users.Updated;

public record UserUpdatedEvent(User User) : INotification;
