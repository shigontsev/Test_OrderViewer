using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderViewer.Common.Entities;
using OrderViewer.Service.Interfaces;

namespace OrderViewer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Product> GetAll() 
        {
            return _productService.GetAllProduct();
        }

        [HttpGet("GetById/{id}")]
        public Product GetById(int id)
        {
            return _productService.GetProduct(id);
        }

        [HttpGet("GetByName/{name}")]
        public Product GetByName(string name)
        {
            return _productService.GetProduct(name);
        }

        [Authorize]
        [HttpPost("Add/{name}/{description}/{price}")]
        public bool Add(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            name = name.Trim();
            description = string.IsNullOrWhiteSpace(description) ?
                "" : description.Trim();
            if (price <= 0)
            {
                return false;
            }
            Product product = new Product()
            {
                Name = name,
                Description = description,
                Price = price
            };

            return _productService.AddProduct(product);
        }

        [Authorize]
        [HttpPut("Update/{id}/{name}/{description}/{price}")]
        public IActionResult Update(int id, string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            name = name.Trim();
            description = string.IsNullOrWhiteSpace(description) ?
                "" : description.Trim();
            if (price <= 0)
            {
                return BadRequest();
            }
            Product product = new Product()
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price
            };

            if (!_productService.UpdateProduct(product))
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _productService.GetProduct(id);
            if (!_productService.DeleteProduct(id))
                return NotFound();
            return Ok(product);
        }
    }
}
