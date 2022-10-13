//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using WebApplication2.Data;
//using WebApplication2.Domain.Entities;
//using WebApplication2.Service;
//using WebApplication5.Service;

//namespace WebApplication5.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UsersController : ControllerBase
//    {
//        private readonly ILogger<UsersController> _logger;

//        public UsersController(ILogger<UsersController> logger)
//        {
//            _logger = logger;
//        }
//        [HttpGet]
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> Get()
//        {
//            var values = await Repository.GetAllValues<User, DataBaseContext>();
//            if (values != null)
//            {
//                foreach (var user in User.Claims)
//                    _logger.LogInformation($"{user.ValueType}: {user.Value}: Status200(GetAllUsers)");
//                return Ok(values);
//            }
//            _logger.LogInformation("Status404: GetAllValues");
//            return NotFound();
//        }
//        [HttpGet("id")]
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
//        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var value = await Repository.GetValueById<User, DataBaseContext>(id);
//            return value != null ?
//                Ok(value) : NotFound();
//        }
//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        [ProducesResponseType(StatusCodes.Status409Conflict)]
//        public async Task<IActionResult> Create(User user)
//        {
//            return await Repository.AddValue<User, DataBaseContext>(user) ?
//                CreatedAtAction(nameof(GetById), user) : Conflict();
//        }
//    }
//}
