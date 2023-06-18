using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public Product GetById(string name)
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

        //[Authorize]
        //[HttpGet("Update/{id}")]
        //public bool Update(int id)
        //{
        //    Product product = _productService.GetProduct(id);
        //    if (product == null)
        //    {
        //        return false;
        //    }

        //    return Update(product.Id, product.Name, product.Description);
        //    //return RedirectToAction("Update", new { id = product.Id, name = product.Name, description = product.Description });
        //}

        //[Authorize]
        //[HttpPut("Update/{id}/{name}/{description}")]
        //public bool Update(int id, string name, string description)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        return false;
        //    }
        //    name = name.Trim();
        //    description = string.IsNullOrWhiteSpace(description) ?
        //        "" : description.Trim();
        //    Product product = new Product()
        //    {
        //        Name = name,
        //        Description = description
        //    };

        //    return _productService.UpdateProduct(product);
        //}

        //[Authorize]
        //[HttpGet("Update/{id}")]
        //public bool Update(int id)
        //{
        //    Product product = _productService.GetProduct(id);
        //    if (product == null)
        //    {
        //        return false;
        //    }

        //    return Update(product.Id, product.Name, product.Description);
        //    //return RedirectToAction("Update", new { id = product.Id, name = product.Name, description = product.Description });
        //}

        [Authorize]
        [HttpPut("Update/{id}/{name}/{description}/{price}")]
        //public IActionResult Update(int id, [FromBody] Product product)
        public IActionResult Update(int id, string name, string description, decimal price)
        {
            //if (product == null || string.IsNullOrWhiteSpace(product.Name))
            //{
            //    return BadRequest();
            //}

            //product.Id = id;
            //bool result = _productService.UpdateProduct(product);

            //if (!result)
            //{
            //    return NotFound();
            //}


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
