using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OrderViewer.Service.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace OrderViewer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserOrderService _userOrderService;

        public HomeController(IUserOrderService userOrderService)
        {
            _userOrderService = userOrderService;
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("SignUp")]
        public bool SignUp(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;
            return _userOrderService.CreateUser(name.Trim(), password.Trim());
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// Example input field
        ///     name : admin
        ///     password : admin
        /// </example>
        [HttpPost("LogIn")]
        public bool LogIn(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;
            var resulte = _userOrderService.IsAuthentication(name.Trim(), password.Trim());
            if (resulte)
            {
                Authenticate(name);
            }
            return resulte;
        }

        [Authorize]
        [HttpPost("LogOut")]
        public bool LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }


        private void Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
