using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Promotions.Remove;

public record RemovePromotionCommand(string Id) : IRequest<BaseResponse>;
