using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Requests;
using MediatR;

namespace ByteTech.Application.Features.Users.Edit;

public record EditUserCommand(string Id, EditUserRequest Request) : IRequest<BaseResponse>;
