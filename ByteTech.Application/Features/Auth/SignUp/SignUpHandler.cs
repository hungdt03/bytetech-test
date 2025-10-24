using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Events.Users.Created;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using ByteTech.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ByteTech.Application.Features.Auth.SignUp;

public class SignUpHandler(IUserRepository userRepository, IMediator mediator) : IRequestHandler<SignUpCommand, BaseResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new UnauthorizedException("Email đã được sử dụng");
        }

        var hasher = new PasswordHasher<object>();
        var hashedPassword = hasher.HashPassword(null!, request.Password);

        var newUser = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = hashedPassword,
            Role = EUserRole.Customer,
            IsLocked = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        await _userRepository.AddAsync(newUser);

        await _mediator.Publish(new UserCreatedEvent(newUser), cancellationToken);

        return new BaseResponse()
        {
            Message = "Đăng ký tài khoản thành công",
            StatusCode = HttpStatusCode.OK,
            Success = true
        };
    }
}
