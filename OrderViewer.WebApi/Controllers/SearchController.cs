using Microsoft.AspNetCore.Mvc;
using OrderViewer.Common.Entities;
using OrderViewer.Service.Interfaces;

namespace OrderViewer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("GetAllUser")]
        public IEnumerable<UserData> GetAllUser()
        {
            return _searchService.GetAllUser();
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _searchService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("GetUserByName/{name}")]
        public IActionResult GetUserByName(string name)
        {
            var user = _searchService.GetUser(name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpGet("GetAllProduct")]
        public IEnumerable<Product> GetAllProduct()
        {
            return _searchService.GetAllProduct();
        }

        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _searchService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }    

            return Ok(product);
        }

        [HttpGet("GetProductByName/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var product = _searchService.GetProduct(name);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetUsersBySubName/{name}")]
        public IEnumerable<UserData> GetUsersBySubName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = string.Empty;
            }

            return _searchService.GetUsersBySubName(name);
        }

        [HttpGet("GetProductsBySubName/{name}")]
        public IEnumerable<Product> GetProductsBySubName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                name = string.Empty;
            }

            return _searchService.GetProductsBySubName(name);
        }

        [HttpGet("FiltrUserProductBySubName/{user_subName}/{product_subName}")]
        public IActionResult FiltrUserProductBySubName(string? user_subName, string? product_subName)
        {
            if (string.IsNullOrWhiteSpace(user_subName))
            {
                user_subName = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(product_subName))
            {
                product_subName = string.Empty;
            }
            var result = _searchService.FiltrUserProductBySubName(user_subName, product_subName);


            return Ok(result);
        }

        [HttpGet("FiltrUserProductById/{user_id}/{product_id}")]
        public IActionResult FiltrUserProductById(int user_id, int product_id)
        {
            var result = _searchService.FiltrUserProductById(user_id, product_id);


            return Ok(result);
        }
    }
}
