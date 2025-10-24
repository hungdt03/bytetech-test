using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Promotions.GetAll;

public record GetAllPromotionsQuery(string SearchText) : IRequest<BaseResponse>;
