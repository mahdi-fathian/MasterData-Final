using MasterData.DalServiceApi.DAL.Repositories;
using MasterData.DalServiceApi.Models;
using MasterData.DalServiceApi.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace MasterData.DalServiceApi.Service.Services;

/// <summary>
/// سرویس استان - Province Service
/// </summary>
public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _repository;
    private readonly ILogger<ProvinceService> _logger;

    public ProvinceService(IProvinceRepository repository, ILogger<ProvinceService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ProvinceDto> CreateAsync(CreateProvinceDto dto)
    {
        try
        {
            _logger.LogInformation("ایجاد استان جدید با نام: {ProvinceName}", dto.Name);

            // اعتبارسنجی
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("نام استان نمی‌تواند خالی باشد");

            if (dto.Name.Length > 50)
                throw new ArgumentException("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد");

            // بررسی تکراری بودن
            if (await _repository.ExistsByNameAsync(dto.Name))
            {
                _logger.LogWarning("استان با نام {ProvinceName} قبلاً ثبت شده است", dto.Name);
                throw new InvalidOperationException($"استان با نام '{dto.Name}' قبلاً ثبت شده است");
            }

            var province = new Province { Name = dto.Name.Trim() };
            var result = await _repository.AddAsync(province);

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت ایجاد شد", result.Id);

            return MapToDto(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در ایجاد استان");
            throw;
        }
    }

    public async Task<ProvinceDto?> GetByIdAsync(int id)
    {
        var province = await _repository.GetByIdAsync(id);
        return province == null ? null : MapToDto(province);
    }

    public async Task<IEnumerable<ProvinceDto>> GetAllAsync()
    {
        var provinces = await _repository.GetAllAsync();
        return provinces.Select(MapToDto);
    }

    public async Task<ProvinceDto> UpdateAsync(int id, UpdateProvinceDto dto)
    {
        try
        {
            _logger.LogInformation("بروزرسانی استان با شناسه: {ProvinceId}", id);

            // اعتبارسنجی
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("نام استان نمی‌تواند خالی باشد");

            if (dto.Name.Length > 50)
                throw new ArgumentException("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد");

            var province = await _repository.GetByIdAsync(id);
            if (province == null)
            {
                _logger.LogWarning("استان با شناسه {ProvinceId} یافت نشد", id);
                throw new KeyNotFoundException($"استان با شناسه {id} یافت نشد");
            }

            // بررسی تکراری بودن
            if (await _repository.ExistsByNameExceptIdAsync(dto.Name, id))
            {
                _logger.LogWarning("استان دیگری با نام {ProvinceName} وجود دارد", dto.Name);
                throw new InvalidOperationException($"استان دیگری با نام '{dto.Name}' وجود دارد");
            }

            province.Name = dto.Name.Trim();
            var result = await _repository.UpdateAsync(province);

            _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت بروزرسانی شد", id);

            return MapToDto(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در بروزرسانی استان");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            _logger.LogInformation("حذف استان با شناسه: {ProvinceId}", id);

            var result = await _repository.DeleteAsync(id);

            if (result)
                _logger.LogInformation("استان با شناسه {ProvinceId} با موفقیت حذف شد", id);
            else
                _logger.LogWarning("استان با شناسه {ProvinceId} یافت نشد", id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در حذف استان");
            throw;
        }
    }

    private static ProvinceDto MapToDto(Province province)
    {
        return new ProvinceDto(province.Id, province.Name, province.CreatedAt, province.UpdatedAt);
    }
}
