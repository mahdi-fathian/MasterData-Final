using MediatR;

namespace MasterData.CQRS.Application.Queries;

/// <summary>
/// پرس‌وجوی دریافت استان با شناسه - Get Province By Id Query
/// </summary>
public record GetProvinceByIdQuery(int Id) : IRequest<ProvinceDto?>;
