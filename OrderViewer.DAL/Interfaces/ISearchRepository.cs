using OrderViewer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.DAL.Interfaces
{
    public interface ISearchRepository
    {
        UserData GetUser(int id);

        UserData GetUser(string username);

        IEnumerable<UserData> GetAllUser();        

        Product GetProduct(int id);

        Product GetProduct(string name);

        IEnumerable<Product> GetAllProduct();

        IEnumerable<Product> GetProductsBySubName(string name);

        IEnumerable<UserData> GetUsersBySubName(string name);
    }
}
