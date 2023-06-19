using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderViewer.Common.Entities;
using OrderViewer.Service.Implementaitons;
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
        public UserData GetUserById(int id)
        {
            return _searchService.GetUser(id);
        }

        [HttpGet("GetUserByName/{name}")]
        public UserData GetUserByName(string name)
        {
            return _searchService.GetUser(name);
        }


        [HttpGet("GetAllProduct")]
        public IEnumerable<Product> GetAllProduct()
        {
            return _searchService.GetAllProduct();
        }

        [HttpGet("GetProductById/{id}")]
        public Product GetProductById(int id)
        {
            return _searchService.GetProduct(id);
        }

        [HttpGet("GetProductByName/{name}")]
        public Product GetProductByName(string name)
        {
            return _searchService.GetProduct(name);
        }

        [HttpGet("GetUsersBySubName/{name}")]
        public IEnumerable<UserData> GetUsersBySubName(string name)
        {
            return _searchService.GetUsersBySubName(name);
        }

        [HttpGet("GetProductsBySubName/{name}")]
        public IEnumerable<Product> GetProductsBySubName(string name)
        {
            return _searchService.GetProductsBySubName(name);
        }
    }
}
