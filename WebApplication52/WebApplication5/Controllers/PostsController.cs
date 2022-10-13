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
    public class PostsController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly DataBaseContext _context;
        public PostsController(ILogger<CitiesController> logger, DataBaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Post>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var values = await Repository.GetAllValues<Post>(_context);
            if (values != null)
            {
                _logger.LogInformation($"REQUEST Status200OK: GetAllPosts");
                return Ok(values);
            }
            _logger.LogInformation("REQUEST Status404NotFound: GetAllPosts");
            return NotFound();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await Repository.GetValueById<Post>(_context, id);
            if (value != null)
            {
                _logger.LogInformation($"REQUEST Status200OK: GetPostById");
                return Ok(value);
            }
            _logger.LogInformation("REQUEST Status404NotFound: GetPostById");
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Post), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(PostWebApi postWebApi)
        {
            if (postWebApi == null)
                return BadRequest("mapPoint was null");
            Post post;
            if (postWebApi.PhotoBase64 != null)
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
                            Convert.FromBase64String(postWebApi.PhotoBase64.Substring(postWebApi.PhotoBase64.IndexOf(',') + 1)))))
                    {
                        var extendedPath = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + path;
                        image.Save(extendedPath, ImageFormat.Png);
                    }
                }
                catch { return BadRequest("Image error"); }
                post = new Post(postWebApi.DateOfCreation);
                post.Title = postWebApi.Title;
                post.Text = postWebApi.Text;
                post.OfficeId = postWebApi.OfficeId;
                post.PhotoPath = "/images/" + path;
            }
            else
            {
                post = new Post(postWebApi.DateOfCreation);
                post.Title = postWebApi.Title;
                post.Text = postWebApi.Text;
                post.OfficeId = postWebApi.OfficeId;
                post.PhotoPath = null;
            }
            try
            {
                await Repository.AddValue<Post>(_context, post);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status409Conflict: GetPostById");
                return Conflict(e.Message);
            }
            _logger.LogInformation($"REQUEST Status201Created: GetPostById");
            return CreatedAtAction(nameof(GetById), postWebApi);
        }
        [HttpPut("id")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, PostWebApi postWebApi)
        {
            if (postWebApi == null)
                return BadRequest("post is null");
            if (id != postWebApi.Id)
                return BadRequest("Id do not equal post.Id");
            Post post;
            if (postWebApi.PhotoBase64 != null)
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
                            Convert.FromBase64String(postWebApi.PhotoBase64.Substring(postWebApi.PhotoBase64.IndexOf(',') + 1)))))
                    {
                        var extendedPath = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + path;
                        image.Save(extendedPath, ImageFormat.Png);
                    }
                }
                catch { return BadRequest("Image error"); }
                post = new Post(postWebApi.DateOfCreation);
                post.Id = postWebApi.Id;
                post.Title = postWebApi.Title;
                post.Text = postWebApi.Text;
                post.OfficeId = postWebApi.OfficeId;
                post.PhotoPath = "/images/" + path;
            }
            else
            {
                post = new Post(postWebApi.DateOfCreation);
                post.Id = postWebApi.Id;
                post.Title = postWebApi.Title;
                post.Text = postWebApi.Text;
                post.OfficeId = postWebApi.OfficeId;
                post.PhotoPath = null;
            }
            try
            {
                await Repository.UpdateValue<Post>(_context, post);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status400BadRequest: GetPostById");
                return BadRequest(e.Message);
            }
            _logger.LogInformation($"REQUEST Status200OK: GetPostById");
            return Ok(post);
        }
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Repository.DeleteValueById<Post>(_context, id);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"REQUEST Status400BadRequest: GetPostById");
                return BadRequest(e.Message);
            }
            _logger.LogInformation($"REQUEST Status200OK: GetPostById");
            return Ok();
        }
    }
}
