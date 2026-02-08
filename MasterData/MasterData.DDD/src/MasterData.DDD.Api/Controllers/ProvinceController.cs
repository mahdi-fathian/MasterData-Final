using MasterData.DDD.Application.DTOs;
using MasterData.DDD.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterData.DDD.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProvinceController : ControllerBase
{
    private readonly IProvinceService _provinceService;
    private readonly ILogger<ProvinceController> _logger;

    public ProvinceController(IProvinceService provinceService, ILogger<ProvinceController> logger)
    {
        _provinceService = provinceService;
        _logger = logger;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.GetAllProvincesAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت لیست استان‌ها");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.GetActiveProvincesAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت لیست استان‌های فعال");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.GetProvinceByIdAsync(id, cancellationToken);

            if (result == null)
                return NotFound(new { message = "استان یافت نشد" });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت استان با شناسه: {ProvinceId}", id);
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }

  
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProvinceRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.CreateProvinceAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در ایجاد استان");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProvinceRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.UpdateProvinceAsync(id, request, cancellationToken);

            if (!result)
                return NotFound(new { message = "استان یافت نشد" });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در بروزرسانی استان");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }


    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.DeactivateProvinceAsync(id, cancellationToken);

            if (!result)
                return NotFound(new { message = "استان یافت نشد" });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در غیرفعال کردن استان");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }

 
    [HttpPatch("{id}/activate")]
    public async Task<IActionResult> Activate(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.ActivateProvinceAsync(id, cancellationToken);

            if (!result)
                return NotFound(new { message = "استان یافت نشد" });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در فعال کردن استان");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.DeleteProvinceAsync(id, cancellationToken);

            if (!result)
                return NotFound(new { message = "استان یافت نشد" });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در حذف استان");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }
}
