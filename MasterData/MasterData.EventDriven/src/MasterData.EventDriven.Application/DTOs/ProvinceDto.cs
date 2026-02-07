namespace MasterData.EventDriven.Application.DTOs;

/// <summary>
/// DTO استان - Province Data Transfer Object
/// </summary>
public record ProvinceDto(
    int Id,
    string Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsDeleted
);

public record RegisterProvinceRequest(string Name);
public record UpdateProvinceRequest(string Name);
