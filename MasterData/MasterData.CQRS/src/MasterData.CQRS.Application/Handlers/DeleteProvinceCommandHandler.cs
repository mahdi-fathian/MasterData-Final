using MasterData.CQRS.Application.Commands;
using MasterData.CQRS.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MasterData.CQRS.Application.Handlers;


public class DeleteProvinceCommandHandler : IRequestHandler<DeleteProvinceCommand, bool>
{
    private readonly IProvinceRepository _repository;
    private readonly ILogger<DeleteProvinceCommandHandler> _logger;

    public DeleteProvinceCommandHandler(
        IProvinceRepository repository,
        ILogger<DeleteProvinceCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("حذف استان با شناسه: {ProvinceId}", request.Id);

            var province = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (province == null)
            {
                _logger.LogWarning("استان با شناسه {ProvinceId} یافت نشد", request.Id);
                return false;
            }

            _repository.Delete(province);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت حذف شد", request.Id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در حذف استان با شناسه: {ProvinceId}", request.Id);
            throw;
        }
    }
}
