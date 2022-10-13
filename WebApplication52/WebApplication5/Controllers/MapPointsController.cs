using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using WebApplication5.Data;
using WebApplication5.Domain.Entities;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapPointsController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly DataBaseContext _context;
        public MapPointsController(ILogger<CitiesController> logger, DataBaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MapPoint>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var values = await Repository.GetAllValues<MapPoint>(_context);
            if (values != null)
            {
                _logger.LogInformation($"REQUEST Status200OK: GetAllMapPoints ");
                return Ok(values);
            }
            _logger.LogInformation("REQUEST Status404NotFound: GetAllMapPoints");
            return NotFound();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(MapPoint), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await Repository.GetValueById<MapPoint>(_context, id);
            if (value != null)
            {
                _logger.LogInformation($"REQUEST Status200OK: GetMapPointById");
                return Ok(value);
            }
            _logger.LogInformation("REQUEST Status404NotFound: GetMapPointById");
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(MapPoint), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(MapPointWebApi mapPointWebApi)
        {
            if (mapPointWebApi == null)
                return BadRequest("mapPoint was null");
            MapPoint mapPoint;
            if (mapPointWebApi.PhotoBase64 != null)
            {
                string path = DateTime.UtcNow.ToString();
                path = path.Replace(".", "_");
                path = path.Replace(" ", "_");
                path = path.Replace(":", "_");
                path = path + ".png";
                try
                {
                    using (Image image = Image.FromStream(
                        new MemoryStream(
                            Convert.FromBase64String(mapPointWebApi.PhotoBase64.Substring(mapPointWebApi.PhotoBase64.IndexOf(',') + 1)))))
                    {
                        var extendedPath = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + path;
                        image.Save(extendedPath, ImageFormat.Png);
                    }
                }
                catch { return BadRequest("Image error"); }
                mapPoint = new MapPoint(mapPointWebApi.Lat,
                    mapPointWebApi.Lng,
                    mapPointWebApi.City,
                    mapPointWebApi.Comments,
                    mapPointWebApi.DateOfCreation,
                    mapPointWebApi.TinkoffCashBack,
                    mapPointWebApi.Likes,
                    mapPointWebApi.DisLikes);
                mapPoint.Title = mapPointWebApi.Title;
                mapPoint.Text = mapPointWebApi.Text;
                mapPoint.PhotoPath = "/images/" + path;
            }
            else
            {
                mapPoint = new MapPoint(mapPointWebApi.Lat,
                    mapPointWebApi.Lng,
                    mapPointWebApi.City,
                    mapPointWebApi.Comments,
                    mapPointWebApi.DateOfCreation,
                    mapPointWebApi.TinkoffCashBack,
                    mapPointWebApi.Likes,
                    mapPointWebApi.DisLikes);
                mapPoint.Title = mapPointWebApi.Title;
                mapPoint.Text = mapPointWebApi.Text;
                mapPoint.PhotoPath = null;
            }
            try
            {
                await Repository.AddValue<MapPoint>(_context, mapPoint);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status409Conflict: GetMapPointById");
                return Conflict(e.Message);
            }
            _logger.LogInformation($"REQUEST Status201Created: GetMapPointById");
            return CreatedAtAction(nameof(GetById), mapPoint);
        }
        [HttpPut("id")]
        [ProducesResponseType(typeof(MapPoint), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, MapPointWebApi mapPointWebApi)
        {
            if (mapPointWebApi == null)
                return BadRequest("mapPointWebApi is null");
            if (id != mapPointWebApi.Id)
                return BadRequest("Id do not equal mapPointWebApi.Id");
            MapPoint mapPoint;
            if (mapPointWebApi.PhotoBase64 != null)
            {
                string path = DateTime.UtcNow.ToString();
                path = path.Replace(".", "_");
                path = path.Replace(" ", "_");
                path = path.Replace(":", "_");
                path = path + ".png";
                try
                {
                    using (Image image = Image.FromStream(
                        new MemoryStream(
                            Convert.FromBase64String(mapPointWebApi.PhotoBase64.Substring(mapPointWebApi.PhotoBase64.IndexOf(',') + 1)))))
                    {
                        var extendedPath = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + path;
                        image.Save(extendedPath, ImageFormat.Png);
                    }
                }
                catch { return BadRequest("Image error"); }
                mapPoint = new MapPoint(mapPointWebApi.Lat,
                    mapPointWebApi.Lng,
                    mapPointWebApi.City,
                    mapPointWebApi.Comments,
                    mapPointWebApi.DateOfCreation,
                    mapPointWebApi.TinkoffCashBack,
                    mapPointWebApi.Likes,
                    mapPointWebApi.DisLikes);
                mapPoint.Id = mapPointWebApi.Id;
                mapPoint.Title = mapPointWebApi.Title;
                mapPoint.Text = mapPointWebApi.Text;
                mapPoint.PhotoPath = "/images/" + path;
            }
            else
            {
                mapPoint = new MapPoint(mapPointWebApi.Lat,
                    mapPointWebApi.Lng,
                    mapPointWebApi.City,
                    mapPointWebApi.Comments,
                    mapPointWebApi.DateOfCreation,
                    mapPointWebApi.TinkoffCashBack,
                    mapPointWebApi.Likes,
                    mapPointWebApi.DisLikes);
                mapPoint.Id = mapPointWebApi.Id;
                mapPoint.Title = mapPointWebApi.Title;
                mapPoint.Text = mapPointWebApi.Text;
                mapPoint.PhotoPath = null;
            }
            try
            {
                await Repository.UpdateValue<MapPoint>(_context, mapPoint);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status400BadRequest: GetMapPointById");
                return BadRequest(e.Message);
            }
            _logger.LogInformation($"REQUEST Status200OK: GetMapPointById");
            return Ok(mapPoint);
        }
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Repository.DeleteValueById<MapPoint>(_context, id);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status400BadRequest: GetMapPointById");
                return BadRequest(e.Message);
            }
            _logger.LogInformation($"REQUEST Status200OK: GetMapPointById");
            return Ok();
        }
    }
}

