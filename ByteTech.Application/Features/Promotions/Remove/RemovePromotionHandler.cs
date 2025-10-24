using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Events.Promotions.Deleted;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using MediatR;

namespace ByteTech.Application.Features.Promotions.Remove;

public class RemovePromotionHandler(
    IPromotionRepository promotionRepository,
    IMediator mediator
) : IRequestHandler<RemovePromotionCommand, BaseResponse>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(RemovePromotionCommand request, CancellationToken cancellationToken)
    {
        var existing = await _promotionRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Không tìm thấy khuyến mại cần xoá");

        await _promotionRepository.DeleteAsync(existing);

        await _mediator.Publish(new PromotionDeletedEvent(request.Id), cancellationToken);

        return new BaseResponse()
        {
            Success = true,
            StatusCode = HttpStatusCode.NoContent,
            Message = "Xoá khuyến mại thành công"
        };
    }
}