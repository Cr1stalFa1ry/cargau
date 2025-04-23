using Core.Models;
using db.Entities;

namespace Presentation.Mappers.user
{
    public static class UsersMapping
    {
        public static User ToUser(this UserEntity user)
        {
            return User.Create(user.Id, user.UserName, user.PasswordHash, user.Email);
        }

        // public static UserEntity ToEntity(this User user)
        // {
        //     return null;
        // }
    }
}
