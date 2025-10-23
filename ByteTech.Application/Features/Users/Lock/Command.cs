using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Users.Lock;

public record Command(string Id) : IRequest<BaseResponse>;
