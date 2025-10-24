using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Events.Promotions.Updated;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using MediatR;

namespace ByteTech.Application.Features.Promotions.Edit;

public class EditPromotionHandler(IPromotionRepository promotionRepository, IMediator mediator)
    : IRequestHandler<EditPromotionCommand, BaseResponse>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(EditPromotionCommand request, CancellationToken cancellationToken)
    {
        var promotion = await _promotionRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Không tìm thấy khuyến mại");

        var existingByCode = await _promotionRepository.GetByCodeAsync(request.EditPromotion.Code);
        if (existingByCode is not null && existingByCode.Id != promotion.Id)
        {
            throw new AppException("Mã khuyến mại đã tồn tại");
        }

        promotion.Name = request.EditPromotion.Name;
        promotion.Code = request.EditPromotion.Code;
        promotion.Description = request.EditPromotion.Description;
        promotion.DiscountType = request.EditPromotion.DiscountType;
        promotion.DiscountValue = request.EditPromotion.DiscountValue;
        promotion.StartDate = request.EditPromotion.StartDate;
        promotion.EndDate = request.EditPromotion.EndDate;
        promotion.IsLimited = request.EditPromotion.IsLimited;
        promotion.LimitPerUser = request.EditPromotion.LimitPerUser;
        promotion.IsActive = request.EditPromotion.IsActive;
        promotion.UpdatedAt = DateTime.UtcNow;

        await _promotionRepository.UpdateAsync(promotion);

        await _mediator.Publish(new PromotionUpdatedEvent(promotion), cancellationToken);

        return new BaseResponse()
        {
            Message = "Cập nhật khuyến mại thành công",
            StatusCode = HttpStatusCode.NoContent,
            Success = true
        };
    }
}