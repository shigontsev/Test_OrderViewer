using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderViewer.Common.Entities;
using OrderViewer.Service.Interfaces;
using OrderViewer.WebApi.ViewModels;

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


        #region errais 
        [HttpGet("GetListOrders")]
        public IActionResult GetListOrders()
        {
            //var ord = _userOrderService.GetOrdersByUserId(user_id);
            //var ord = _userOrderService.GetAllOrders();
            var orders_t = (from or in _userOrderService.GetAllOrders()
                            group or by or.OrderId).ToList();
            if (orders_t == null || orders_t.Count < 1)
            {
                return BadRequest();
            }

            var orders = new List<OrderViewModel>();
            foreach (var g in orders_t)
            {
                var vm = new OrderViewModel()
                {
                    Id = g.Key,
                    UserId = g.First().UserId,
                    UserName = g.First().UserName,
                    Products = new List<Product>()
                };
                foreach (var item in g)
                {
                    vm.Products.Add(new Product()
                    {
                        Id = item.ProductId,
                        Name = item.ProductName,
                        Description = item.Description,
                        Price = item.Price,
                    });
                }
                orders.Add(vm);
            }

            return Ok(orders);
        }

        [HttpGet("GetListOrdersByUserId/{user_id}")]
        public IActionResult GetListOrdersByUserId(int user_id)
        {
            //var ord = _userOrderService.GetOrdersByUserId(user_id);
            //var ord = _userOrderService.GetAllOrders();
            var orders_t = (from or in _userOrderService.GetOrdersByUserId(user_id)
                         group or by or.OrderId).ToList();
            if (orders_t == null || orders_t.Count < 1)
            {
                return BadRequest();
            }

            var orders = new List<OrderViewModel>();
            foreach (var g in orders_t) 
            {
                var vm = new OrderViewModel()
                {
                    Id = g.Key,
                    UserId = g.First().UserId,
                    UserName = g.First().UserName,
                    Products = new List<Product>()
                };
                foreach (var item in g.OrderBy(p=>p.Price))
                {
                    vm.Products.Add(new Product()
                    {
                        Id = item.ProductId,
                        Name = item.ProductName,
                        Description = item.Description,
                        Price = item.Price,
                    });
                }
                orders.Add(vm);
            }

            return Ok(orders);
        }

        [HttpGet("GetOrderById/{order_id}")]
        public IActionResult GetOrdersById(int order_id)
        {
            //var order_t = (from or in _userOrderService.GetOrderById(order_id)
                            //group or by or.OrderId);
            var order_t = _userOrderService.GetOrderById(order_id).OrderBy(p=>p.Price).ToList();
            if (order_t == null || order_t.Count < 1)
            {
                return BadRequest();
            }

            var order = new OrderViewModel()
            {
                Id = order_t.First().OrderId,
                UserId = order_t.First().UserId,
                UserName = order_t.First().UserName,
                Products = new List<Product>()
            };
            foreach (var pr in order_t)
            {
                order.Products.Add(new Product()
                {
                    Id = pr.ProductId,
                    Name = pr.ProductName,
                    Description = pr.Description,
                    Price = pr.Price,
                });
            }

            return Ok(order);
        }

        #endregion errais 

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
