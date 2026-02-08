using MediatR;

namespace MasterData.CQRS.Application.Queries;


public record GetAllProvincesQuery : IRequest<IEnumerable<ProvinceDto>>;


public record ProvinceDto(int Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt);
