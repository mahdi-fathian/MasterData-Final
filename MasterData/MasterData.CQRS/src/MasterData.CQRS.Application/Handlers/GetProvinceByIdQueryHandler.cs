using MasterData.CQRS.Application.Interfaces;
using MasterData.CQRS.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MasterData.CQRS.Application.Handlers;


public class GetProvinceByIdQueryHandler : IRequestHandler<GetProvinceByIdQuery, ProvinceDto?>
{
    private readonly IProvinceReadRepository _repository;
    private readonly ILogger<GetProvinceByIdQueryHandler> _logger;

    public GetProvinceByIdQueryHandler(
        IProvinceReadRepository repository,
        ILogger<GetProvinceByIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ProvinceDto?> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("دریافت استان با شناسه: {ProvinceId}", request.Id);

            var province = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (province == null)
            {
                _logger.LogWarning("استان با شناسه {ProvinceId} یافت نشد", request.Id);
                return null;
            }

            return new ProvinceDto(province.Id, province.Name, province.CreatedAt, province.UpdatedAt);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت استان با شناسه: {ProvinceId}", request.Id);
            throw;
        }
    }
}
