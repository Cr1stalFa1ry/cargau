using Core.Models;
using db.Entities;
using Core.Enum;

namespace Presentation.Mappers.user
{
    public static class UsersMapping 
    {
        public static User ToModel(this UserEntity user)
        {
            return User.Create(
                user.Id,
                (Roles) user.RoleId!,
                user.UserName,
                user.Email,
                user.PasswordHash
            );       
        }

        public static UserEntity ToEntity(this User user, RoleEntity role)
        {
            return new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                RoleId = (int)user.Role,
                Role = role
            };
        }
    }
}
