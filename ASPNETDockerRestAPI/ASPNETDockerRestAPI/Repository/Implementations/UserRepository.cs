using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Repository.User;
using Serilog;
using System.Text;

namespace ASPNETDockerRestAPI.Repository.Implementations
{
    public class UserRepository(MySqlContext dbContext) : IUserRepository
    {
        public UserModel ValidateCredentials(UserDto userDto)
        {
            var encryptedPassword = ComputeHash(userDto.Password, new SHA256CryptoServiceProvider());

            return dbContext.Users.FirstOrDefault(u => u.UserName.Equals(userDto.UserName) && u.Password.Equals(encryptedPassword));
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

        private static string ComputeHash(string password, SHA256CryptoServiceProvider sHA256CryptoServiceProvider)
        {
            var inputBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = sHA256CryptoServiceProvider.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
