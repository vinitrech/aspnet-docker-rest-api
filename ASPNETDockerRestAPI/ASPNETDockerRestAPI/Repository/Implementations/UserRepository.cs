using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Repository.User;
using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace ASPNETDockerRestAPI.Repository.Implementations
{
    public class UserRepository(MySqlContext dbContext) : IUserRepository
    {
        public UserModel ValidateCredentials(UserDto userDto)
        {
            var encryptedPassword = ComputeHash(userDto.Password, SHA256.Create());

            return dbContext.Users.SingleOrDefault(u => u.UserName.Equals(userDto.UserName) && u.Password.Equals(encryptedPassword));
        }

        public UserModel ValidateCredentials(string username)
        {
            return dbContext.Users.SingleOrDefault(u => u.UserName.Equals(username));
        }

        public bool RevokeToken(string username)
        {
            var user = dbContext.Users.SingleOrDefault(u => u.UserName.Equals(username));

            if (user is null)
            {
                return false;
            }

            user.RefreshToken = null;
            dbContext.SaveChanges();

            return true;
        }

        public UserModel RefreshUserInfo(UserModel userModel)
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
