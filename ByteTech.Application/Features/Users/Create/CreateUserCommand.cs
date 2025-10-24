using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Users.Create;

public record CreateUserCommand(string Email, string FullName, string Password) : IRequest<BaseResponse>;

