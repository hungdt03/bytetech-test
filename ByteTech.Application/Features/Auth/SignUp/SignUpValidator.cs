using FluentValidation;

namespace ByteTech.Application.Features.Auth.SignUp;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Vui lòng nhập địa chỉ email")
            .EmailAddress().WithMessage("Email không hợp lệ");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Vui lòng nhập họ tên");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Vui lòng nhập mật khẩu")
            .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự");
    }
}