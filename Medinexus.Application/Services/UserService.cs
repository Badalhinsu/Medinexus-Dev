using Medinexus.Application.Models.User;
using Medinexus.Application.Services.Interfaces;
using Medinexus.Domain.Entities;
using Medinexus.Domain.Interfaces;
using Medinexus.API.Helpers;
using Medinexus.Application.Models.Token;
namespace Medinexus.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHelper _jwtHelper;

        public UserService(IUserRepository userRepository, JwtTokenHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<UserResponseModel> AddNewUserAsync(UserRequestModel model)
        {
            try
            {
                UserMaster user = new UserMaster
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MobileNo = model.MobileNo,
                    Email = model.Email,
                    UserName = model.UserName,
                    Password = PasswordHelper.HashPassword(model.Password),
                    CreatedAt = DateTime.UtcNow
                };

                UserMaster createdUser = await _userRepository.AddNewUserAsync(user);

                UserResponseModel response = MapToResponseModel(createdUser);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserResponseModel?> GetUserByIdAsync(int id)
        {
            try
            {
                UserMaster? user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                    return null;

                UserResponseModel response = MapToResponseModel(user);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserResponseModel?> UpdateUserDetailAsync(UserRequestModel model)
        {
            try
            {
                UserMaster updatedUser = new UserMaster
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MobileNo = model.MobileNo,
                    Email = model.Email,
                    UserName = model.UserName,
                    Password = model.Password,
                    ModifiedAt = DateTime.UtcNow
                };

                UserMaster? result = await _userRepository.UpdateUserDetailAsync(updatedUser);
                if (result == null)
                    return null;

                UserResponseModel response = MapToResponseModel(result);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                bool result = await _userRepository.DeleteUserAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TokenResponseModel?> LoginAsync(string username, string password)
        {
            try
            {
                UserMaster? user = await _userRepository.GetByUserNameOrEmailAsync(username);
                if (user == null || !PasswordHelper.VerifyPassword(password, user.Password))
                    return null;

                string accessToken = _jwtHelper.GenerateAccessToken(user);
                string refreshToken = _jwtHelper.GenerateRefreshToken();
                DateTime refreshExpiry = DateTime.UtcNow.AddDays(7);

                await _userRepository.UpdateRefreshTokenAsync(user.Id, refreshToken, refreshExpiry);

                return new TokenResponseModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    AccessTokenExpiry = DateTime.UtcNow.AddMinutes(15),
                    RefreshTokenExpiry = refreshExpiry
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TokenResponseModel?> GetByRefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);
            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                return null;

            string newAccessToken = _jwtHelper.GenerateAccessToken(user);
            string newRefreshToken = _jwtHelper.GenerateRefreshToken();
            DateTime newRefreshExpiry = DateTime.UtcNow.AddDays(7);

            await _userRepository.UpdateRefreshTokenAsync(user.Id, newRefreshToken, newRefreshExpiry);

            return new TokenResponseModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(15),
                RefreshTokenExpiry = newRefreshExpiry
            };
        }

        private UserResponseModel MapToResponseModel(UserMaster user)
        {
            UserResponseModel response = new UserResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNo = user.MobileNo,
                Email = user.Email,
                UserName = user.UserName,
                CreatedAt = user.CreatedAt,
                ModifiedAt = user.ModifiedAt
            };

            return response;
        }
    }
}
