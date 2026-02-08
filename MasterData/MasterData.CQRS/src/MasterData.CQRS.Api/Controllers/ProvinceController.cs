using MasterData.CQRS.Application.Commands;
using MasterData.CQRS.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MasterData.CQRS.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProvinceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProvinceController> _logger;

    public ProvinceController(IMediator mediator, ILogger<ProvinceController> logger)
    {
        _mediator = mediator;
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
            var query = new GetAllProvincesQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطا در دریافت لیست استان‌ها");
            return StatusCode(500, new { message = "خطای داخلی سرور" });
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetProvinceByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

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
    public async Task<IActionResult> Register([FromBody] RegisterProvinceRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = new RegisterProvinceCommand(request.Name);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
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
            var command = new UpdateProvinceCommand(id, request.Name);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result)
                return NotFound(new { message = "استان یافت نشد" });

            return NoContent();
        }
        catch (InvalidOperationException ex)
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
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteProvinceCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

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


public record RegisterProvinceRequest(string Name);


public record UpdateProvinceRequest(string Name);
