using ByteTech.Domain.Entities;
using MediatR;

namespace ByteTech.Application.Events.Users.Created;

public record UserCreatedEvent(User User) : INotification;
