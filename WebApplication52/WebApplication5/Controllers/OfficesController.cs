using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Domain.Entities;
using WebApplication5.Domain.Entities;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly DataBaseContext _context;
        public OfficesController(ILogger<CitiesController> logger, DataBaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Office>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var values = await Repository.GetAllValues<Office>(_context);
            if (values != null)
            {
                _logger.LogInformation($"REQUEST Status200OK: GetAllOffices ");
                return Ok(values);
            }
            _logger.LogInformation("REQUEST Status404NotFound: GetAllOffices");
            return NotFound();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Office), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await Repository.GetValueById<Office>(_context, id);
            if (value != null)
            {
                _logger.LogInformation($"REQUEST Status200OK: GetOfficeById");
                return Ok(value);
            }
            _logger.LogInformation("REQUEST Status404NotFound: GetOfficeById");
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Office), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(Office office)
        {
            try
            {
                await Repository.AddValue<Office>(_context, office);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status409Conflict: GetOfficeById");
                return Conflict(e.Message);
            }
            _logger.LogInformation($"REQUEST Status201Created: GetOfficeById");
            return CreatedAtAction(nameof(GetById), office);
        }
        [HttpPut("id")]
        [ProducesResponseType(typeof(Office), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Office office)
        {
            if (id != office.Id)
                return BadRequest("Id do not equal office.Id");
            try
            {
                await Repository.UpdateValue<Office>(_context, office);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status400BadRequest: GetOfficeById");
                return BadRequest(e.Message);
            }
            _logger.LogInformation($"REQUEST Status200OK: GetOfficeById");
            return Ok(office);
        }
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Repository.DeleteValueById<Office>(_context, id);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status400BadRequest: GetOfficeById");
                return BadRequest(e.Message);
            }
            _logger.LogInformation($"REQUEST Status200OK: GetOfficeById");
            return Ok();
        }
    }
}
