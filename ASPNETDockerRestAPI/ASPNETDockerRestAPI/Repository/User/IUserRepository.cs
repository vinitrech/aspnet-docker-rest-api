using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Repository.User
{
    public interface IUserRepository
    {
        UserModel ValidateCredentials(UserDto userDto);
        UserModel RefreshUserInfo(UserModel userModel);
    }
}
