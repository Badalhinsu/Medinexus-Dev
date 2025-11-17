using Medinexus.Application.Models;
using Medinexus.Application.Models.Token;
using Medinexus.Application.Models.User;
using Medinexus.Domain.Entities;

namespace Medinexus.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> AddNewUserAsync(UserRequestModel model);
        Task<UserResponseModel?> GetUserByIdAsync(int id);
        Task<UserResponseModel?> UpdateUserDetailAsync(UserRequestModel model);
        Task<bool> DeleteUserAsync(int id);
        Task<TokenResponseModel?> LoginAsync(string usernameOrEmail, string password);
        Task<TokenResponseModel?> GetByRefreshTokenAsync(string refreshToken);
    }
}
