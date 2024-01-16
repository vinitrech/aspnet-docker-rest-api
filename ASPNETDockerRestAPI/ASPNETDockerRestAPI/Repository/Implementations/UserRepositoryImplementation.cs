using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;
using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace ASPNETDockerRestAPI.Repository.Implementations
{
    public class UserRepositoryImplementation(MySqlContext dbContext) : IUserRepository
    {
        public UserModel? ValidateCredentials(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                return null;
            }

            var encryptedPassword = ComputeHash(userDto.Password, SHA256.Create());

            return dbContext.Users.SingleOrDefault(u => string.Equals(u.UserName, userDto.UserName) && string.Equals(u.Password, encryptedPassword));
        }

        public UserModel? ValidateCredentials(string? username)
        {
            return dbContext.Users.SingleOrDefault(u => string.Equals(u.UserName, username));
        }

        public bool RevokeToken(string? username)
        {
            var user = dbContext.Users.SingleOrDefault(u => string.Equals(u.UserName, username));

            if (user is null)
            {
                return false;
            }

            user.RefreshToken = null;
            dbContext.SaveChanges();

            return true;
        }

        public UserModel? RefreshUserInfo(UserModel userModel)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.Id.Equals(userModel.Id));

            if (user is null)
            {
                return null;
            }

            try
            {
                dbContext.Update(user);
                dbContext.SaveChanges();

                return user;
            }
            catch (Exception)
            {
                Log.Error($"There was an error refreshing user {user.UserName} info");
                throw;
            }
        }

        private static string ComputeHash(string password, HashAlgorithm hashAlgorithm)
        {
            var inputBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = hashAlgorithm.ComputeHash(inputBytes);
            var stringBuilder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                stringBuilder.Append(item.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
