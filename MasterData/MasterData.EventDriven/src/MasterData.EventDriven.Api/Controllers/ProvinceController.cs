using MasterData.EventDriven.Application.DTOs;
using MasterData.EventDriven.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterData.EventDriven.Api.Controllers;

/// <summary>
/// کنترلر استان - Province Controller
/// </summary>
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

    /// <summary>
    /// دریافت تمام استان‌ها - Get all provinces
    /// </summary>
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

    /// <summary>
    /// دریافت استان با شناسه - Get province by id
    /// </summary>
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

    /// <summary>
    /// ثبت استان جدید - Register new province
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterProvinceRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provinceService.RegisterProvinceAsync(request, cancellationToken);
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
            _logger.LogError(ex, "خطا در ثبت استان");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }

    /// <summary>
    /// بروزرسانی استان - Update province
    /// </summary>
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

    /// <summary>
    /// حذف استان - Delete province
    /// </summary>
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
