using MasterData.CQRS.Application.Interfaces;
using MasterData.CQRS.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MasterData.CQRS.Application.Handlers;


public class GetAllProvincesQueryHandler : IRequestHandler<GetAllProvincesQuery, IEnumerable<ProvinceDto>>
{
    private readonly IProvinceReadRepository _repository;
    private readonly ILogger<GetAllProvincesQueryHandler> _logger;

    public GetAllProvincesQueryHandler(
        IProvinceReadRepository repository,
        ILogger<GetAllProvincesQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<ProvinceDto>> Handle(GetAllProvincesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("دریافت لیست تمام استان‌ها");

            var provinces = await _repository.GetAllAsync(cancellationToken);

            return provinces.Select(p => new ProvinceDto(p.Id, p.Name, p.CreatedAt, p.UpdatedAt));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت لیست استان‌ها");
            throw;
        }
    }
}
