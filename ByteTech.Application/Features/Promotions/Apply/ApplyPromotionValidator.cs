using FluentValidation;

namespace ByteTech.Application.Features.Promotions.Apply;

public class ApplyPromotionValidator: AbstractValidator<ApplyPromotionCommand>
{
    public ApplyPromotionValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId không được để trống");

        RuleFor(x => x.PromotionCode)
            .NotEmpty().WithMessage("Mã khuyến mãi không được để trống")
            .MaximumLength(50).WithMessage("Mã khuyến mãi không được vượt quá 50 ký tự");
    }
}
