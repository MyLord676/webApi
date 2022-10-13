//using Microsoft.AspNetCore.Authentication.OAuth;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Text;
//using WebApplication2.Data;
//using WebApplication2.Domain.Entities;
//using WebApplication5.Domain.Entities;
//using WebApplication5.Service;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebApplication5.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        // POST api/<LoginController>
//        [HttpPost]
//        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<IActionResult> Login(UserAuth userAuth)
//        {
//            var values = await Repository.GetAllValues<User, DataBaseContext>();
//            var user = values.Where(user => user.NickName == userAuth.NickName).First();
//            if (user == null)
//                return NotFound();
//            if (user.PasswordHash != userAuth.Password)
//                return Unauthorized();

//            var claims = new[]
//            {
//                new Claim(ClaimsIdentity.DefaultNameClaimType, user.NickName),
//                new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"),
//            };
//            var token = new JwtSecurityToken(
//                issuer: JwtConfig.Issuer,
//                audience: JwtConfig.Audience,
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(60),
//                notBefore: DateTime.UtcNow,
//                signingCredentials: new SigningCredentials(
//                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Key)),
//                    SecurityAlgorithms.HmacSha256
//                    )
//                );
//            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

//            //var response = new
//            //{
//            //    access_token = tokenString,
//            //    username = user.NickName
//            //};

//            return Ok(tokenString);
//        }
//    }
//}
