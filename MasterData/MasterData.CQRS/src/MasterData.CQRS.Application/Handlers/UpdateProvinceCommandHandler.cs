using MasterData.CQRS.Application.Commands;
using MasterData.CQRS.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MasterData.CQRS.Application.Handlers;

/// <summary>
/// پردازشگر دستور بروزرسانی استان - Update Province Command Handler
/// </summary>
public class UpdateProvinceCommandHandler : IRequestHandler<UpdateProvinceCommand, bool>
{
    private readonly IProvinceRepository _repository;
    private readonly ILogger<UpdateProvinceCommandHandler> _logger;

    public UpdateProvinceCommandHandler(
        IProvinceRepository repository,
        ILogger<UpdateProvinceCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("بروزرسانی استان با شناسه: {ProvinceId}", request.Id);

            var province = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (province == null)
            {
                _logger.LogWarning("استان با شناسه {ProvinceId} یافت نشد", request.Id);
                return false;
            }

            // بررسی تکراری بودن نام (به جز خود رکورد)
            var exists = await _repository.ExistsByNameExceptIdAsync(request.Name, request.Id, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("استان دیگری با نام {ProvinceName} وجود دارد", request.Name);
                throw new InvalidOperationException($"استان دیگری با نام '{request.Name}' وجود دارد");
            }

            province.UpdateName(request.Name);
            _repository.Update(province);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت بروزرسانی شد", request.Id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در بروزرسانی استان با شناسه: {ProvinceId}", request.Id);
            throw;
        }
    }
}
