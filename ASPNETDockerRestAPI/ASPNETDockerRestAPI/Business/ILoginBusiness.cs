using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Business
{
    public interface ILoginBusiness
    {
        TokenDto ValidateCredentials(UserDto userDto);
        TokenDto ValidateCredentials(TokenDto tokenDto);
        public bool RevokeToken(string username);
    }
}
