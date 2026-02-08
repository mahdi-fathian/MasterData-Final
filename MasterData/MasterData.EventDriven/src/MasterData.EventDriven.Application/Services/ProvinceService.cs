using MasterData.EventDriven.Application.DTOs;
using MasterData.EventDriven.Application.Interfaces;
using MasterData.EventDriven.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace MasterData.EventDriven.Application.Services;

public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _repository;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<ProvinceService> _logger;

    public ProvinceService(
        IProvinceRepository repository,
        IEventPublisher eventPublisher,
        ILogger<ProvinceService> logger)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task<ProvinceDto> RegisterProvinceAsync(RegisterProvinceRequest request, CancellationToken cancellationToken = default)
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

            var province = Province.Register(request.Name);
            await _repository.AddAsync(province, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            // انتشار رویدادها
            await _eventPublisher.PublishEventsAsync(province.Events, cancellationToken);
            province.ClearEvents();

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت ثبت شد", province.Id);

            return MapToDto(province);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در ثبت استان با نام: {ProvinceName}", request.Name);
            throw;
        }
    }

    public async Task<ProvinceDto?> GetProvinceByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var province = await _repository.GetByIdAsync(id, cancellationToken);
        return province == null ? null : MapToDto(province);
    }

    public async Task<IEnumerable<ProvinceDto>> GetAllProvincesAsync(CancellationToken cancellationToken = default)
    {
        var provinces = await _repository.GetAllAsync(cancellationToken);
        return provinces.Select(MapToDto);
    }

    public async Task<bool> UpdateProvinceAsync(int id, UpdateProvinceRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("بروزرسانی استان با شناسه: {ProvinceId}", id);

            var province = await _repository.GetByIdAsync(id, cancellationToken);
            if (province == null)
            {
                _logger.LogWarning("استان با شناسه {ProvinceId} یافت نشد", id);
                return false;
            }

            // بررسی تکراری بودن نام
            var exists = await _repository.ExistsByNameExceptIdAsync(request.Name, id, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("استان دیگری با نام {ProvinceName} وجود دارد", request.Name);
                throw new InvalidOperationException($"استان دیگری با نام '{request.Name}' وجود دارد");
            }

            province.UpdateName(request.Name);
            _repository.Update(province);
            await _repository.SaveChangesAsync(cancellationToken);

            // انتشار رویدادها
            await _eventPublisher.PublishEventsAsync(province.Events, cancellationToken);
            province.ClearEvents();

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت بروزرسانی شد", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در بروزرسانی استان با شناسه: {ProvinceId}", id);
            throw;
        }
    }

    public async Task<bool> DeleteProvinceAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var province = await _repository.GetByIdAsync(id, cancellationToken);
            if (province == null)
                return false;

            province.Delete();
            _repository.Update(province);
            await _repository.SaveChangesAsync(cancellationToken);

            // انتشار رویدادها
            await _eventPublisher.PublishEventsAsync(province.Events, cancellationToken);
            province.ClearEvents();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در حذف استان با شناسه: {ProvinceId}", id);
            throw;
        }
    }

    private static ProvinceDto MapToDto(Province province)
    {
        return new ProvinceDto(
            province.Id,
            province.Name,
            province.CreatedAt,
            province.UpdatedAt,
            province.IsDeleted
        );
    }
}
