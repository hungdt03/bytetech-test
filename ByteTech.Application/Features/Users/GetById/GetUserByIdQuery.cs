using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Users.GetById;

public record GetUserByIdQuery(string Id) : IRequest<BaseResponse>;
