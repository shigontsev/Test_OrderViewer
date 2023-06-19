using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderViewer.Common.Entities;
using OrderViewer.Service.Interfaces;

namespace OrderViewer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUserOrderService _userOrderService;

        public OrderController(IUserOrderService userOrderService)
        {
            _userOrderService = userOrderService;
        }

        [Authorize]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(IEnumerable<Product> products)
        {
            if (products is null || products.Count() == 0)
            {
                BadRequest();
            }
            if (!_userOrderService.CreateOrder(User.Identity.Name, products)) {
                return NotFound();
            }

            return Ok(products);
        }

        [Authorize]
        [HttpGet("GetAllOrdersShort")]
        public IActionResult GetAllOrdersShort()
        {
            var result = _userOrderService.GetAllOrdersShort();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetAllOrdersFull")]
        public IActionResult GetAllOrdersFull()
        {
            var result = _userOrderService.GetAllOrdersFull();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetAllOrdersShortByUserId/{user_id}")]
        public IActionResult GetAllOrdersShortByUserId(int user_id)
        {
            var result = _userOrderService.GetAllOrdersShortByUserId(user_id);

            if (result is null || result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetAllOrdersFullByUserId/{user_id}")]
        public IActionResult GetAllOrdersFullByUserId(int user_id)
        {
            var result = _userOrderService.GetAllOrdersFullByUserId(user_id);

            if (result is null || result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
