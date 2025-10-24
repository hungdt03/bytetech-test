using System;
using ByteTech.Application.Common;
using MediatR;

namespace ByteTech.Application.Features.Users.Unlock;

public record UnlockUserCommand(string Id) : IRequest<BaseResponse>;
