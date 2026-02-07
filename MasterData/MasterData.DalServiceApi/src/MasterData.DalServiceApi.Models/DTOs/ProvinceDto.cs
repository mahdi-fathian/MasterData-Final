namespace MasterData.DalServiceApi.Models.DTOs;

public record ProvinceDto(int Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt);
public record CreateProvinceDto(string Name);
public record UpdateProvinceDto(string Name);
