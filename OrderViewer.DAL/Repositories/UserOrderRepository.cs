using OrderViewer.Common.Entities;
using OrderViewer.Common.Helpers;
using OrderViewer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer.DAL.Repositories
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDBContext _db;
        //public UserOrderRepository(ApplicationDBContext db) 
        //{
        //    _db = db;
        //}
        public UserOrderRepository()
        {
            _db = new ApplicationDBContext();
        }

        public bool CreateUser(string login, string password)
        {
            //var u = GetUser(login);
            if (string.IsNullOrWhiteSpace(login) || GetUser(login) is not null)
            {
                return false;
            }
            var user = new UserData()
            {
                Name = login,
                HashPass = Hasher.HashPassword(password)
            };
            _db.UserData.Add(user);
            _db.SaveChanges();

            return true;
        }

        public bool IsAuthentication(string login, string password)
        {
            var accountData = _db.UserData.FirstOrDefault(x=>x.Name == login);
            if (accountData != null)
            {
                return Hasher.VerifyHashedPassword(accountData.HashPass, password);
            }
            return false;
        }

        public UserData GetUser(int id)
        {
            //UserData user = (UserData)(from u in _db.UserData
            //                           where u.Id == id
            //                           select new
            //                           {
            //                               Id = u.Id,
            //                               Name = u.Name
            //                           });
            //return user;
            UserData user = _db.UserData.FirstOrDefault(x => x.Id == id);
            return new UserData()
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public UserData GetUser(string username)
        {
            UserData user = _db.UserData.FirstOrDefault(x => x.Name == username);
            //UserData user = (from u in _db.UserData
            //                           where u.Name == username
            //                           select new
            //                           {
            //                               Id = u.Id,
            //                               Name = u.Name
            //                           }) as UserData;
            return new UserData()
            {
                Id = user.Id,
                Name = user.Name
            };
            //return user;
        }

        public IEnumerable<UserData> GetUsers()
        {
            var users = 
                (IEnumerable<UserData>)(from u in _db.UserData                                                                  
                                        select new
                                        {
                                            Id = u.Id,
                                            Name = u.Name
                                        });
            return users;
        }

    }
}
