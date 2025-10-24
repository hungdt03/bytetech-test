using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Auth.SignIn;

public record SignInQuery(string Email, string Password) : IRequest<BaseResponse>;
