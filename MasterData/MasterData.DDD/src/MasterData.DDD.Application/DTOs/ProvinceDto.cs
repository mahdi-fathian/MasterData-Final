namespace MasterData.DDD.Application.DTOs;


public record ProvinceDto(
    int Id,
    string Name,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateProvinceRequest(string Name);


public record UpdateProvinceRequest(string Name);
