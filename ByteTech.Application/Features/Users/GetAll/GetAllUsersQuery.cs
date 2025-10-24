using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Users.GetAll;

public record GetAllUsersQuery(string SearchText) : IRequest<BaseResponse>;

