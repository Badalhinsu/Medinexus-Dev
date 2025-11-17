using Medinexus.Domain.Entities;

namespace Medinexus.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserMaster> AddNewUserAsync(UserMaster userMaster);
        Task<UserMaster?> GetUserByIdAsync(int id);
        Task<UserMaster?> UpdateUserDetailAsync(UserMaster userMaster);
        Task<bool> DeleteUserAsync(int id);
        Task<UserMaster?> GetByUserNameOrEmailAsync(string usernameOrEmail);
        Task<UserMaster?> GetByRefreshTokenAsync(string refreshToken);
        Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiryTime);
    }
}
