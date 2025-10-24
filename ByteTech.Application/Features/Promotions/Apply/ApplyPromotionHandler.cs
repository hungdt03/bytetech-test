using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Events.PromotionUsages.Created;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using MediatR;

namespace ByteTech.Application.Features.Promotions.Apply;

public class ApplyPromotionHandler(
    IPromotionRepository promotionRepository,
    IPromotionUsageRepository promotionUsageRepository,
    IMediator mediator
) : IRequestHandler<ApplyPromotionCommand, BaseResponse>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IPromotionUsageRepository _promotionUsageRepository = promotionUsageRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(ApplyPromotionCommand request, CancellationToken cancellationToken)
    {
        var promotion = await _promotionRepository.GetByCodeAsync(request.PromotionCode) ?? throw new NotFoundException("Mã khuyến mãi không tồn tại");
        if (!promotion.IsActive)
        {
            throw new AppException("Mã khuyến mãi không còn hiệu lực");
        }
        
        var now = DateTime.UtcNow;
        if (now < promotion.StartDate || now > promotion.EndDate)
        {
            throw new AppException("Mã khuyến mãi đã hết hạn hoặc chưa bắt đầu");
        }

        if (promotion.IsLimited)
        {
            var usageCount = await _promotionUsageRepository.CountByUserAndPromotionAsync(request.UserId, promotion.Id);
            if (usageCount >= promotion.LimitPerUser)
            {
                throw new AppException("Bạn đã sử dụng mã khuyến mãi này vượt quá giới hạn cho phép");
            }
        }

        var usage = new PromotionUsage
        {
            UserId = request.UserId,
            PromotionId = promotion.Id,
            UsedAt = DateTime.UtcNow,
            DiscountAmount = promotion.DiscountValue,
            DiscountType = promotion.DiscountType,
            CreatedAt = DateTime.Now,
        };

        await _promotionUsageRepository.AddAsync(usage);

        await _mediator.Publish(new PromotionUsageCreatedEvent(usage), cancellationToken);

        return new BaseResponse()
        {
            Message = "Áp dụng mã khuyến mãi thành công",
            StatusCode = HttpStatusCode.OK,
            Success = true
        };
    }
}