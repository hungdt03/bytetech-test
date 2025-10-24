using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Events.Users.Updated;
using ByteTech.Application.Exceptions;
using ByteTech.Application.IRepositories;
using MediatR;

namespace ByteTech.Application.Features.Users.Edit;

public class EditUserHandler(IUserRepository userRepository, IMediator mediator)
    : IRequestHandler<EditUserCommand, BaseResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<BaseResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Người dùng không tồn tại");
        user.FullName = request.Request.FullName;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);

        await _mediator.Publish(new UserUpdatedEvent(user));

        return new BaseResponse()
        {
            Message = "Cập nhật thông tin người dùng thành công",
            StatusCode = HttpStatusCode.NoContent,
            Success = true
        };
    }
}