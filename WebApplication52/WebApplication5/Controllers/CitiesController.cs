using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data;
using WebApplication5.Domain.Entities;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly DataBaseContext _context;
        public CitiesController(ILogger<CitiesController> logger, DataBaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<City>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var values = await Repository.GetAllValues<City>(_context);
            if (values != null)
            {
                _logger.LogInformation($"REQUEST Status200OK: GetAllCities ");
                return Ok(values);
            }
            _logger.LogInformation("REQUEST Status404NotFound: GetAllCities");
            return NotFound();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await Repository.GetValueById<City>(_context, id);
            if (value != null)
            {
                _logger.LogInformation($"REQUEST Status200OK: GetCityById");
                return Ok(value);
            }
            _logger.LogInformation("REQUEST Status404NotFound: GetCityById");
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(City), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(City city)
        {
            try
            {
                await Repository.AddValue<City>(_context, city);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status409Conflict: GetCityById");
                return Conflict(e.Message);
            }
            _logger.LogInformation($"REQUEST Status201Created: GetCityById");
            return CreatedAtAction(nameof(GetById), city);
        }
        [HttpPut("id")]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, City city)
        {
            if (id != city.Id)
                return BadRequest("Id do not equal city.Id");
            try
            {
                await Repository.UpdateValue<City>(_context, city);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status400BadRequest: GetCityById");
                return BadRequest(e.Message);
            }
            _logger.LogInformation($"REQUEST Status200OK: GetCityById");
            return Ok(city);
        }
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Repository.DeleteValueById<City>(_context, id);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status400BadRequest: GetCityById");
                return BadRequest(e.Message);
            }
            _logger.LogInformation($"REQUEST Status200OK: GetCityById");
            return Ok();
        }
    }
}
