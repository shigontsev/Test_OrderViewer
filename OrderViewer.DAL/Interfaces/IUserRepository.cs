using OrderViewer.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.DAL.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(string login, string password);

        bool IsAuthentication(string login, string password);

        UserData GetUser(int id);

        UserData GetUser(string username);

        IEnumerable<UserData> GetUsers();
    }
}
