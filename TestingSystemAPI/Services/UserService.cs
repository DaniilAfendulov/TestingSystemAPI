using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingSystemAPI.Models.DataBase;

namespace TestingSystemAPI.Services
{
    public class UserService : IUserService
    {
        private UserDbContext _userDbContext;

        public UserService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public User FindUser(string login)
        {
            return FindUser(u => u.Login == login);
        }

        public User FindUser(Func<User, bool> predicate)
        {
            if (_userDbContext.Users == null) return null;
            return _userDbContext.Users.FirstOrDefault(predicate);
        }

        public bool CheckPassword(User user, string password)
        {
            return PasswordHelper.VerifyHashedPassword(user.HashedPassword, password);
        }

        public void AddUser(string login, string password)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);
            _userDbContext.Users.Add(new User(login, hashedPassword));
            _userDbContext.SaveChanges();
        }

        public async Task AddUserAsync(string login, string password)
        {
            string hashedPassword = PasswordHelper.HashPassword(password);
            await _userDbContext.Users.AddAsync(new User(login, hashedPassword));
            await _userDbContext.SaveChangesAsync();
        }
    }
}
