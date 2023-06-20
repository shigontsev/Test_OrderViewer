using OrderViewer.Common.Entities;

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
