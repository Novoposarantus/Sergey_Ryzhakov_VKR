using Models.DtoModels;
using Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        List<UserDto> All { get; }
        List<UserDto> Users { get; }
        List<UserDto> Admins { get; }
        UserModel GetUser(int userId);
        UserModel GetUser(string userName);
        UserModel GetUser(string userName, string password);
        void SaveNewUser(UserModel user);
    }
}
