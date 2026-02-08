using MasterData.DDD.Application.DTOs;
using MasterData.DDD.Domain.Aggregates.ProvinceAggregate;
using MasterData.DDD.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace MasterData.DDD.Application.Services;


public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProvinceService> _logger;

    public ProvinceService(
        IProvinceRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<ProvinceService> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ProvinceDto> CreateProvinceAsync(CreateProvinceRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("ایجاد استان جدید با نام: {ProvinceName}", request.Name);

            // بررسی تکراری بودن نام
            var exists = await _repository.ExistsByNameAsync(request.Name, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("استان با نام {ProvinceName} قبلاً ثبت شده است", request.Name);
                throw new InvalidOperationException($"استان با نام '{request.Name}' قبلاً ثبت شده است");
            }

            var province = Province.Create(request.Name);
            await _repository.AddAsync(province, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت ایجاد شد", province.Id);

            return MapToDto(province);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در ایجاد استان با نام: {ProvinceName}", request.Name);
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

    public async Task<IEnumerable<ProvinceDto>> GetActiveProvincesAsync(CancellationToken cancellationToken = default)
    {
        var provinces = await _repository.GetActiveProvincesAsync(cancellationToken);
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
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت بروزرسانی شد", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در بروزرسانی استان با شناسه: {ProvinceId}", id);
            throw;
        }
    }

    public async Task<bool> DeactivateProvinceAsync(int id, CancellationToken cancellationToken = default)
    {
        var province = await _repository.GetByIdAsync(id, cancellationToken);
        if (province == null)
            return false;

        province.Deactivate();
        _repository.Update(province);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ActivateProvinceAsync(int id, CancellationToken cancellationToken = default)
    {
        var province = await _repository.GetByIdAsync(id, cancellationToken);
        if (province == null)
            return false;

        province.Activate();
        _repository.Update(province);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteProvinceAsync(int id, CancellationToken cancellationToken = default)
    {
        var province = await _repository.GetByIdAsync(id, cancellationToken);
        if (province == null)
            return false;

        _repository.Remove(province);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static ProvinceDto MapToDto(Province province)
    {
        return new ProvinceDto(
            province.Id,
            province.Name.Value,
            province.IsActive,
            province.CreatedAt,
            province.UpdatedAt
        );
    }
}
