using ByteTech.Application.Contracts.Requests;
using FluentValidation;

namespace ByteTech.Application.Features.Promotions.Edit;

public class EditPromotionValidator : AbstractValidator<EditPromotionCommand>
{
    public EditPromotionValidator()
    {
        RuleFor(x => x.EditPromotion)
            .NotNull().WithMessage("Dữ liệu chỉnh sửa không được để trống");

        When(x => x.EditPromotion != null, () =>
        {
            RuleFor(x => x.EditPromotion.Name)
                .NotEmpty().WithMessage("Tên khuyến mại không được để trống")
                .MaximumLength(200).WithMessage("Tên khuyến mại không được vượt quá 200 ký tự");

            RuleFor(x => x.EditPromotion.Code)
                .NotEmpty().WithMessage("Mã khuyến mại không được để trống")
                .MaximumLength(50).WithMessage("Mã khuyến mại không được vượt quá 50 ký tự");

            RuleFor(x => x.EditPromotion.DiscountValue)
                .GreaterThan(0).WithMessage("Giá trị khuyến mại phải lớn hơn 0");

            RuleFor(x => x.EditPromotion.StartDate)
                .LessThan(x => x.EditPromotion.EndDate)
                .WithMessage("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");

            RuleFor(x => x.EditPromotion.EndDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Ngày kết thúc phải lớn hơn ngày hiện tại");

            RuleFor(x => x.EditPromotion.LimitPerUser)
                .GreaterThan(0).When(x => x.EditPromotion.IsLimited)
                .WithMessage("Số lượng giới hạn mỗi người dùng phải lớn hơn 0");

            RuleFor(x => x.EditPromotion.DiscountType)
                .IsInEnum().WithMessage("Kiểu giảm giá không hợp lệ");
        });
    }
}
