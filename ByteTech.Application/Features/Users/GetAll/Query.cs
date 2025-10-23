using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Users.GetAll;

public record Query(string SearchText) : IRequest<BaseResponse>;

