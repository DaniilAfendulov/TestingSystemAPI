using System.ComponentModel.DataAnnotations;

namespace TestingSystemAPI.Models.DataBase
{
    public class User
    {
        public User(string login, string hashedPassword)
        {
            Login = login;
            HashedPassword = hashedPassword;
            Role = "student";
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
