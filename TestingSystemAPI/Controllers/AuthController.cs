using Common;
using Common.Data.Content;
using Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestingSystemAPI.Models;
using TestingSystemAPI.Models.DataBase;
using TestingSystemAPI.Services;
using Microsoft.AspNetCore.Authentication;

namespace TestingSystemAPI.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class AuthController
    {
        private readonly IUserService _userService;
        private IHttpContextAccessor _httpContext;

        public AuthController(IUserService userService, IHttpContextAccessor httpContext)
        {
            _userService = userService;
            _httpContext = httpContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Auth(string login, string password)
        {
            User user = _userService.FindUser(login);
            if (user == null) return new BadRequestObjectResult("пользователь не найден");
            if (_userService.CheckPassword(user, password))
            {
                #region JWT Auth
                //var tokenString = GenerateJwtToken(login);
                //return new OkObjectResult(new { Token = tokenString, Message = "Success" });
                #endregion
                #region Cookie Auth
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", 
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
                return new OkResult();
                #endregion
            }
            return new BadRequestObjectResult("пользователь не найден");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registr(string login, string password)
        {
            User user = _userService.FindUser(login);
            if (user != null) return new BadRequestObjectResult(new { Message = "login already exist" });

            await _userService.AddUserAsync(login, password);

            return await Auth(login, password);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new OkResult();
        }


        //private string GenerateJwtToken(string userName)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] { new Claim("id", userName) }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        Issuer = _configuration["Jwt:Issuer"],
        //        Audience = _configuration["Jwt:Audience"],
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}


    }
}
