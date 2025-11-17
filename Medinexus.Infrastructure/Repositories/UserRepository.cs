using Medinexus.Domain.Entities;
using Medinexus.Domain.Interfaces;
using Medinexus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Medinexus.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region UserMaster Methods
        public async Task<UserMaster> AddNewUserAsync(UserMaster userMaster)
        {
            try
            {
                await _appDbContext.UserMasters.AddAsync(userMaster);
                await _appDbContext.SaveChangesAsync();
                return userMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserMaster?> GetUserByIdAsync(int id)
        {
            return await _appDbContext.UserMasters
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<UserMaster> UpdateUserDetailAsync(UserMaster userMaster)
        {
            UserMaster? existingUser = await _appDbContext.UserMasters
                                            .FirstOrDefaultAsync(x => x.Id == userMaster.Id);
            if (existingUser == null)
                return null;
            
            // Update only mutable fields
            existingUser.FirstName = userMaster.FirstName;
            existingUser.LastName = userMaster.LastName;
            existingUser.MobileNo = userMaster.MobileNo;
            existingUser.Email = userMaster.Email;
            existingUser.ModifiedAt = userMaster.ModifiedAt;

            //_appDbContext.UserMasters.Update(userMaster);
            await _appDbContext.SaveChangesAsync();
            return userMaster;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var existingUser = await _appDbContext.UserMasters
                    .FirstOrDefaultAsync(x => x.Id == id);
                _appDbContext.Remove(existingUser);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserMaster?> GetByUserNameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                return await _appDbContext.UserMasters.FirstOrDefaultAsync(x =>x.UserName == usernameOrEmail || x.Email == usernameOrEmail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<UserMaster?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _appDbContext.UserMasters
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiryTime)
        {
            var user = await _appDbContext.UserMasters.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = expiryTime;
                await _appDbContext.SaveChangesAsync();
            }
        }


        #endregion
    }
}
