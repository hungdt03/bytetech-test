using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Users.GetById;

public record Query(string Id) : IRequest<BaseResponse>;
