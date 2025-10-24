using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Promotions.GetUseds;

public record GetUsedPromotionsQuery(string UserId) : IRequest<BaseResponse>;
