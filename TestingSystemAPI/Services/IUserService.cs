using System;
using System.Threading.Tasks;
using TestingSystemAPI.Models.DataBase;

namespace TestingSystemAPI.Services
{
    public interface IUserService
    {
        public User FindUser(string login);
        public User FindUser(Func<User, bool> predicate);
        public bool CheckPassword(User user, string password);
        public void AddUser(string login, string password);
        public Task AddUserAsync(string login, string password);
    }
}
