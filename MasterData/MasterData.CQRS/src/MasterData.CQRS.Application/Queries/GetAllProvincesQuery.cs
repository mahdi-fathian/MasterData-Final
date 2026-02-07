using MediatR;

namespace MasterData.CQRS.Application.Queries;

/// <summary>
/// پرس‌وجوی دریافت تمام استان‌ها - Get All Provinces Query
/// </summary>
public record GetAllProvincesQuery : IRequest<IEnumerable<ProvinceDto>>;

/// <summary>
/// DTO استان - Province Data Transfer Object
/// </summary>
public record ProvinceDto(int Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt);
