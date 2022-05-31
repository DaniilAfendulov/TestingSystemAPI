namespace TestingSystemAPI.Models.DataBase
{
    public class UserGroups
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int Group { get; set; }
    }
}
