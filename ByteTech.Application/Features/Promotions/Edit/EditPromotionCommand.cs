using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Requests;
using MediatR;

namespace ByteTech.Application.Features.Promotions.Edit;

public record EditPromotionCommand(string Id, EditPromotionRequest EditPromotion) : IRequest<BaseResponse>;

