using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Users.Lock;

public record LockUserCommand(string Id) : IRequest<BaseResponse>;
