using Core.Models;
using db.Entities;

namespace Presentation.Mappers.user
{
    public static class UsersMapping
    {
        public static User ToModel(this UserEntity user)
        {
            return User.Create(user.Id, user.UserName, user.PasswordHash, user.Email, Core.Enum.Roles.Admin);
        }

        //public static UserEntity ToEntity(this User user)
        //{
        //    return new UserEntity();
        //}
    }
}
