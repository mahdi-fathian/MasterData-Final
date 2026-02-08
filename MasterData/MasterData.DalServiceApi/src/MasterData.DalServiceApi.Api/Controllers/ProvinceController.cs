using MasterData.DalServiceApi.Models.DTOs;
using MasterData.DalServiceApi.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace MasterData.DalServiceApi.Api.Controllers;


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
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _provinceService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت لیست استان‌ها");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _provinceService.GetByIdAsync(id);

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
    public async Task<IActionResult> Create([FromBody] CreateProvinceDto dto)
    {
        try
        {
            var result = await _provinceService.CreateAsync(dto);
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
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProvinceDto dto)
    {
        try
        {
            var result = await _provinceService.UpdateAsync(id, dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _provinceService.DeleteAsync(id);

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
