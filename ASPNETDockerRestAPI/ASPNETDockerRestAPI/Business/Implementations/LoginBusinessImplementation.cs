using ASPNETDockerRestAPI.Configurations;
using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Repository.User;
using ASPNETDockerRestAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ASPNETDockerRestAPI.Business.Implementations
{
    public class LoginBusinessImplementation(TokenConfiguration tokenConfiguration, IUserRepository userRepository, ITokenService tokenService) : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public TokenDto ValidateCredentials(UserDto userDto)
        {
            var userModel = userRepository.ValidateCredentials(userDto);

            if (userModel is null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new(JwtRegisteredClaimNames.UniqueName, userDto.UserName)
            };

            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();

            userModel.RefreshToken = refreshToken;
            userModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(tokenConfiguration.DaysToExpiry);

            var createDate = DateTime.Now;
            var expirationDate = createDate.AddMinutes(tokenConfiguration.Minutes);

            userRepository.RefreshUserInfo(userModel);

            return new(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT), accessToken, refreshToken);
        }
    }
}
