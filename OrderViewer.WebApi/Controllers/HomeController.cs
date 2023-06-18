using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderViewer.DAL.Repositories;
using OrderViewer.Service.Implementaitons;
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
        //public HomeController()
        //{
        //    _userOrderService = new UserOrderService( new UserOrderRepository());
        //}

        //[HttpGet("SignUp")]
        //public IActionResult SignUp()
        //{

        //}

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
        [HttpPost("Login")]
        public bool Login(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;
            var resulte = _userOrderService.IsAuthentication(name.Trim(), password.Trim());
            if (resulte)
            {
                //context.Session.SetString("name", "Tom");
                Authenticate(name);
            }
            //FormsAuthentication.SetAuthCookie(name, createPersistentCookie: true);
            return resulte;
        }

        [Authorize]
        [HttpPost("SignOut")]
        public bool SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }

        //[Authorize]
        //[HttpPost("NameAuth")]
        //public string NameAuth()
        //{
        //    //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
        //    return User.Identity.Name==null?"Никто не зареган": User.Identity.Name;
        //}


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
