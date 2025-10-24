using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.IRepositories;
using ByteTech.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ByteTech.Application.Mappers;

[Mapper]
public partial class PromotionUsageMapper(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    [MapPropertyFromSource(target: nameof(PromotionUsageResponse.FullName), Use = nameof(MapFullName))]
    public partial PromotionUsageResponse ToResponse(PromotionUsage promotionUsage);

    [UserMapping(Default = false)]
    public string MapFullName(PromotionUsage promotionUsage)
    {
        var user = _userRepository.GetByIdSync(promotionUsage.UserId);
        return user?.FullName ?? string.Empty;
    }
}
