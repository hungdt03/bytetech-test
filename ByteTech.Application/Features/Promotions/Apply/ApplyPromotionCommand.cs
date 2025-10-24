using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Promotions.Apply;

public record ApplyPromotionCommand(string UserId, string PromotionCode) : IRequest<BaseResponse>;
