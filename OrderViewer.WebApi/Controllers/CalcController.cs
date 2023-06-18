using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrderViewer.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        
        [HttpGet(Name = "GetCalcTest")]
        public int CalculateTest(int a, int b)
        {
            return a+b;
        }

        [HttpGet("GetUsers",Name = "GetUsers")]
        public IList<User> GetUsers()
        {
            IList<User> users = new List<User>()
            {
                new User() { Id = 1, Name = "Fedor"},
                new User() { Id = 2, Name = "Max"}
            };
            return users;
        }

        [HttpGet("GetUsersDef", Name = "GetUsersDef")]
        public IList<User> GetUsersDef(IList<User> users)
        {
            //IList<User> users = new List<User>()
            //{
            //    new User() { Id = 1, Name = "Fedor"},
            //    new User() { Id = 2, Name = "Max"}
            //};
            return users;
        }
    }

    public class User 
    { 
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
