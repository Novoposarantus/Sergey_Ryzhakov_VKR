using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DtoModels
{
    public class UserDto
    {
        public UserDto() { }

        public UserDto(UserModel user)
        {
            Id = user.Id;
            UserName = user.UserName;
            RoleId = user.RoleId;
            Role = user.Role;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
    }
}
