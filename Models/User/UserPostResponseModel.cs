using smart_dental_webapi.Entity;

namespace smart_dental_webapi.Models.User
{
    public class UserPostResponseModel
    {
        public UserEntity User { get; set; }
        public string Token { get; set; }
    }
}
