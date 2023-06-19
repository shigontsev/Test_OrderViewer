using OrderViewer.Common.Entities;
using OrderViewer.DAL.Interfaces;
using OrderViewer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.Service.Implementaitons
{
    public class SearchService : ISearchService
    {
        private ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository) 
        {
            _searchRepository = searchRepository;
        }

        public UserData GetUser(int id)
        {
            return _searchRepository.GetUser(id);
        }

        public UserData GetUser(string username)
        {
            return _searchRepository.GetUser(username);
        }

        public IEnumerable<UserData> GetAllUser()
        {
            return _searchRepository.GetAllUser();
        }

        public Product GetProduct(int id)
        {
            return _searchRepository.GetProduct(id);
        }

        public Product GetProduct(string name)
        {
            return _searchRepository.GetProduct(name);
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _searchRepository.GetAllProduct();
        }

        public IEnumerable<Product> GetProductsBySubName(string name)
        {
            return _searchRepository.GetProductsBySubName(name);
        }

        public IEnumerable<UserData> GetUsersBySubName(string name)
        {
            return _searchRepository.GetUsersBySubName(name);
        }

        public IEnumerable<OrderInfoFull> FiltrUserProductBySubName(string user_subName, string product_subName)
        {
            return _searchRepository.FiltrUserProductBySubName(user_subName, product_subName);
        }

        public IEnumerable<OrderInfoFull> FiltrUserProductById(int user_id, int product_id)
        {
            return _searchRepository.FiltrUserProductById(user_id, product_id);
        }
    }
}
