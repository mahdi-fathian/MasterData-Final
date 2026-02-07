using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly ILogger<ProvinceController> _logger;
        private readonly MyDbContext _db;
        private readonly DbSet<Province> _provinces;

        public ProvinceController(ILogger<ProvinceController> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
            _provinces = _db.Provinces;
        }

        [HttpPost]
        [Route("add-province")]
        public async Task<IActionResult> SaveAsync([FromBody] Province pr)
        {
            var province = new Province() { Name = pr.Name };
            await _provinces.AddAsync(province);
            await _db.SaveChangesAsync();
            return Created(null as Uri, province);
        }

        [HttpPut]
        [Route("update-province")]
        public async Task<IActionResult> UpdateAsync([FromBody] Province province)
        {
            var dbItem = await _provinces.FirstOrDefaultAsync(x => x.Id == province.Id);
            if (dbItem == null)
                return NotFound();
            dbItem.Name = province.Name;
            _provinces.Update(dbItem);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("delete-province")]
        public async Task<IActionResult> DeleteAsync([FromBody] int id)
        {
            var dbItem = await _provinces.FirstOrDefaultAsync(x => x.Id == id);
            if (dbItem == null)
                return NotFound();
            _provinces.Remove(dbItem);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<IEnumerable<Province>>> Get()
        {
            return Ok(await _provinces.ToListAsync());
        }

        
    }
}
