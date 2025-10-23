using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using ByteTech.Application.Mappers;
using ByteTech.Application.Services.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ByteTech.Application.Features.Auth.SignIn;

public class Handler(IUserRepository userRepository, IJwtService jwtService) : IRequestHandler<Query, BaseResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;

    private static readonly UserMapper Mapper = new();

    public async Task<BaseResponse> Handle(Query request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email) ?? throw new UnauthorizedException("Thông tin đăng nhập không đúng");

        var hasher = new PasswordHasher<object>();
        var result = hasher.VerifyHashedPassword(null!, user.PasswordHash, request.Password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedException("Thông tin đăng nhập không đúng");
        }

        if(user.IsLocked)
        {
            throw new UnauthorizedException("Tài khoản của bạn đã bị khóa.");
        }

        var accessToken = _jwtService.GenerateToken(user);

        return new DataResponse<AuthResponse>()
        {
            Message = "Đăng nhập thành công",
            StatusCode = HttpStatusCode.OK,
            Data = new AuthResponse(
                AccessToken: accessToken,
                User: Mapper.ToResponse(user)
            )
        };
    }
}
