using smart_dental_webapi.Entity;

namespace smart_dental_webapi.Repositories
{
    public class UserRepository
    {
        public static UserEntity Get(string username, string password)
        {
            var users = new List<UserEntity>
            {
                new UserEntity { Id = 1, Username = "admin", Password = "admin", Role = "manager" }
            };

            return users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower());
        }
    }
}
