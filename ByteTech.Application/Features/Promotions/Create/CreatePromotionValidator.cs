using FluentValidation;

namespace ByteTech.Application.Features.Promotions.Create;

public class CreatePromotionValidator : AbstractValidator<CreatePromotionCommand>
{
    public CreatePromotionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên khuyến mại không được để trống")
            .MaximumLength(200).WithMessage("Tên khuyến mại không được vượt quá 200 ký tự");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Mã khuyến mại không được để trống")
            .MaximumLength(50).WithMessage("Mã khuyến mại không được vượt quá 50 ký tự");

        RuleFor(x => x.DiscountValue)
            .GreaterThan(0).WithMessage("Giá trị khuyến mại phải lớn hơn 0");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");

        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Ngày kết thúc phải lớn hơn ngày hiện tại");

        RuleFor(x => x.LimitPerUser)
            .GreaterThan(0).When(x => x.IsLimited)
            .WithMessage("Số lượng giới hạn mỗi người dùng phải lớn hơn 0");

        RuleFor(x => x.DiscountType)
            .IsInEnum().WithMessage("Kiểu giảm giá không hợp lệ");
    }
}