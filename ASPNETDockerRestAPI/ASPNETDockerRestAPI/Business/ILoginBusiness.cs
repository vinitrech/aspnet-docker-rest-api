using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business
{
    public interface ILoginBusiness
    {
        TokenDto ValidateCredentials(UserDto userDto);
    }
}
