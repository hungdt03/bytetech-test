using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Promotions.GetActives;

public record GetActivePromotionsQuery() : IRequest<BaseResponse>;
