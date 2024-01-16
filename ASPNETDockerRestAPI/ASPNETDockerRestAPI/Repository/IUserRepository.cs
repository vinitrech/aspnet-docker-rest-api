using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;

namespace ASPNETDockerRestAPI.Repository
{
    public interface IUserRepository
    {
        UserModel? ValidateCredentials(UserDto userDto);
        UserModel? ValidateCredentials(string? username);
        UserModel? RefreshUserInfo(UserModel userModel);
        bool RevokeToken(string? username);
    }
}
