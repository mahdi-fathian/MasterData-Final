namespace MasterData.DDD.Application.DTOs;

/// <summary>
/// DTO استان - Province Data Transfer Object
/// </summary>
public record ProvinceDto(
    int Id,
    string Name,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

/// <summary>
/// درخواست ایجاد استان - Create Province Request
/// </summary>
public record CreateProvinceRequest(string Name);

/// <summary>
/// درخواست بروزرسانی استان - Update Province Request
/// </summary>
public record UpdateProvinceRequest(string Name);
