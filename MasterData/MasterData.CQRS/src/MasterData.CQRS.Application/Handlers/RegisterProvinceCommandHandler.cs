using MasterData.CQRS.Application.Commands;
using MasterData.CQRS.Application.Interfaces;
using MasterData.CQRS.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MasterData.CQRS.Application.Handlers;


public class RegisterProvinceCommandHandler : IRequestHandler<RegisterProvinceCommand, RegisterProvinceCommandResult>
{
    private readonly IProvinceRepository _repository;
    private readonly ILogger<RegisterProvinceCommandHandler> _logger;

    public RegisterProvinceCommandHandler(
        IProvinceRepository repository,
        ILogger<RegisterProvinceCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<RegisterProvinceCommandResult> Handle(RegisterProvinceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("ثبت استان جدید با نام: {ProvinceName}", request.Name);

            // بررسی تکراری بودن نام
            var exists = await _repository.ExistsByNameAsync(request.Name, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("استان با نام {ProvinceName} قبلاً ثبت شده است", request.Name);
                throw new InvalidOperationException($"استان با نام '{request.Name}' قبلاً ثبت شده است");
            }

            var province = new Province(request.Name);
            await _repository.AddAsync(province, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت ثبت شد", province.Id);

            return new RegisterProvinceCommandResult(province.Id, province.Name, province.CreatedAt);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در ثبت استان با نام: {ProvinceName}", request.Name);
            throw;
        }
    }
}
