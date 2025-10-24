using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Auth.SignUp;

public record SignUpCommand(string Email, string Password, string FullName) : IRequest<BaseResponse>;
