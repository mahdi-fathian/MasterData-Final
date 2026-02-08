namespace MasterData.EventDriven.Application.DTOs;

public record ProvinceDto(
    int Id,
    string Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsDeleted
);

public record RegisterProvinceRequest(string Name);
public record UpdateProvinceRequest(string Name);
