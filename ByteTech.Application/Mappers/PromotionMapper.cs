using ByteTech.Application.Contracts.Responses;
using ByteTech.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ByteTech.Application.Mappers;

[Mapper]
public partial class PromotionMapper
{
    public partial PromotionResponse ToResponse(Promotion promotion);
}
