using ASPNETDockerRestAPI.Configurations;
using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Repository;
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

        public TokenDto ValidateCredentials(TokenDto tokenDto)
        {
            var accessToken = tokenDto.AccessToken;
            var refreshToken = tokenDto.RefreshToken;
            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity!.Name;
            var userModel = userRepository.ValidateCredentials(username);
            var createDate = DateTime.Now;
            var expirationDate = createDate.AddMinutes(tokenConfiguration.Minutes);

            if (userModel is null || !userModel.RefreshToken.Equals(refreshToken) || userModel.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return null;
            }

            accessToken = tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = tokenService.GenerateRefreshToken();
            userRepository.RefreshUserInfo(userModel);

            return new(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT), accessToken, refreshToken);
        }

        public bool RevokeToken(string username)
        {
            return userRepository.RevokeToken(username);
        }
    }
}
