using ByteTech.Application.Common;
using ByteTech.Application.Events.Users.Updated;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using MediatR;

namespace ByteTech.Application.Features.Users.Unlock;

public class UnlockUserHandler(IUserRepository userRepository, IMediator mediator) : IRequestHandler<UnlockUserCommand, BaseResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException("Không tìm thấy người dùng");
            
        user.IsLocked = false;
        await _userRepository.UpdateAsync(user);

        await _mediator.Publish(new UserUpdatedEvent(user), cancellationToken);

        return new BaseResponse
        {
            Message = "Mở khóa tài khoản người dùng thành công",
            StatusCode = System.Net.HttpStatusCode.NoContent,
            Success = true
        };
    }
}
