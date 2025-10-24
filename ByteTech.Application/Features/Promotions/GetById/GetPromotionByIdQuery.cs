using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Promotions.GetById;

public record GetPromotionByIdQuery(string Id) : IRequest<BaseResponse>;