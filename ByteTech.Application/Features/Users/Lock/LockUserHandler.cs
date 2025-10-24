using ByteTech.Application.Common;
using ByteTech.Application.Events.Users.Updated;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using ByteTech.Domain.Enums;
using MediatR;

namespace ByteTech.Application.Features.Users.Lock;

public class LockUserHandler(IUserRepository userRepository, IMediator mediator) : IRequestHandler<LockUserCommand, BaseResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(LockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Không tìm thấy người dùng");

        if(user.Role == EUserRole.Admin)
        {
            throw new UnauthorizedException("Không thể khóa tài khoản Admin");
        }

        user.IsLocked = true;
        await _userRepository.UpdateAsync(user);

        await _mediator.Publish(new UserUpdatedEvent(user), cancellationToken);

        return new BaseResponse
        {
            Message = "Khóa tài khoản người dùng thành công",
            StatusCode = System.Net.HttpStatusCode.NoContent,
            Success = true
        };
    }
}
