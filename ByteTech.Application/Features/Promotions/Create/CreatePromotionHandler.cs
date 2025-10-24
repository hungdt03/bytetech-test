using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Events.Promotions.Created;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using MediatR;

namespace ByteTech.Application.Features.Promotions.Create;

public class CreatePromotionHandler(IPromotionRepository promotionRepository, IMediator mediator) : IRequestHandler<CreatePromotionCommand, BaseResponse>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
    {
        var existingByCode = await _promotionRepository.GetByCodeAsync(request.Code);
        if (existingByCode is not null)
        {
            throw new AppException("Mã khuyến mại đã tồn tại");
        }

        var newPromotion = new Promotion()
        {
            Code = request.Code,
            Description = request.Description,
            DiscountType = request.DiscountType,
            DiscountValue = request.DiscountValue,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsLimited = request.IsLimited,
            LimitPerUser = request.LimitPerUser,
            IsActive = request.IsActive,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await _promotionRepository.AddAsync(newPromotion);

        await _mediator.Publish(new PromotionCreatedEvent(newPromotion), cancellationToken);

        return new BaseResponse()
        {
            Message = "Tạo khuyến mại thành công",
            StatusCode = HttpStatusCode.Created,
            Success = true
        };
    }
}
