using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class GicsClassificationMapper
    {
        public static GicsClassificationDTO ToDto(GicsClassification gics) => new()
        {
            SectorId = gics.SectorId,
            IndustryGroupId = gics.IndustryGroupId,
            IndustryId = gics.IndustryId,
            SubIndustryId = gics.SubIndustryId
        };

        public static GicsClassification ToEntity(GicsClassificationDTO dto) => new()
        {
            SectorId = dto.SectorId,
            IndustryGroupId = dto.IndustryGroupId,
            IndustryId = dto.IndustryId,
            SubIndustryId = dto.SubIndustryId
        };
    }

}
