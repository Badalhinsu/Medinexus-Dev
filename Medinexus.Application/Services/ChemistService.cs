using Medinexus.API.Helpers;
using Medinexus.Application.Models.Chemist;
using Medinexus.Application.Models.User;
using Medinexus.Application.Services.Interfaces;
using Medinexus.Domain.Entities;
using Medinexus.Domain.Interfaces;

namespace Medinexus.Application.Services
{
    public class ChemistService : IChemistService
    {
        private readonly IChemistRepository _chemistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChemistService(IChemistRepository chemistRepository, IUserRepository userRepository,IUnitOfWork unitOfWork)
        {
            _chemistRepository = chemistRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        #region Chemist Operations

        public async Task<ChemistResponseModel?> AddNewChemistDetailAsync(ChemistRequestModel chemistRequest)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                ChemistMaster chemistEntity = MapToChemistEntity(chemistRequest);
                chemistEntity.CreatedAt = DateTime.UtcNow;
                ChemistMaster createdChemist = await _chemistRepository.AddNewChemistAsync(chemistEntity);
                UserResponseModel? createdUserResponse = null;
                if (chemistRequest.UserDetail != null)
                {
                    UserMaster newUser = new UserMaster
                    {
                        FirstName = chemistRequest.UserDetail.FirstName,
                        LastName = chemistRequest.UserDetail.LastName,
                        MobileNo = chemistRequest.UserDetail.MobileNo,
                        Email = chemistRequest.UserDetail.Email,
                        UserName = chemistRequest.UserDetail.UserName,
                        Password = PasswordHelper.HashPassword(chemistRequest.UserDetail.Password),
                        CreatedAt = DateTime.UtcNow,
                        ChemistId = createdChemist.Id
                    };

                    UserMaster createdUser = await _userRepository.AddNewUserAsync(newUser);
                    createdUserResponse = new UserResponseModel
                    {
                        Id = createdUser.Id,
                        FirstName = createdUser.FirstName,
                        LastName = createdUser.LastName,
                        MobileNo = createdUser.MobileNo,
                        Email = createdUser.Email,
                        UserName = createdUser.UserName,
                        CreatedAt = createdUser.CreatedAt,
                        ModifiedAt = createdUser.ModifiedAt
                    };

                }
                ChemistResponseModel chemistResponse =  MapToChemistResponse(createdChemist);
                chemistResponse.UserDetails = createdUserResponse;
                await _unitOfWork.CommitAsync();
                return chemistResponse;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw ex;
            }
        }

        public async Task<ChemistResponseModel?> GetChemistDetailByIdAsync(int chemistId)
        {
            ChemistMaster? chemistEntity = await _chemistRepository.GetChemistByIdAsync(chemistId);
            return chemistEntity == null ? null : MapToChemistResponse(chemistEntity);
        }

        public async Task<ChemistResponseModel?> UpdateChemistDetailAsync(ChemistRequestModel chemistRequest)
        {
            ChemistMaster chemistEntity = MapToChemistEntity(chemistRequest);
            chemistEntity.ModifiedAt = DateTime.UtcNow;

            ChemistMaster? updatedChemist = await _chemistRepository.UpdateChemistAsync(chemistEntity);
            return updatedChemist == null ? null : MapToChemistResponse(updatedChemist);
        }

        public async Task<bool> DeleteChemistDetailAsync(int chemistId)
        {
            return await _chemistRepository.DeleteChemistAsync(chemistId);
        }

        #endregion

        #region Private Mapping Helpers

        private ChemistMaster MapToChemistEntity(ChemistRequestModel model)
        {
            return new ChemistMaster
            {
                Id = model.Id,
                CompanyName = model.CompanyName,
                AddressLine1 = model?.AddressLine1,
                AddressLine2 = model?.AddressLine2,
                City = model?.City,
                Pincode = model?.Pincode,
                MobileNo = model?.MobileNo,
                GstNo = model?.GstNo,
                GstIssueDate = model?.GstIssueDate,
                TinNo = model?.TinNo,
                TinIssueDate = model?.TinIssueDate,
                CstNo = model?.CstNo,
                CstIssueDate = model?.CstIssueDate,
                DrugLicenceNo1 = model?.DrugLicenceNo1,
                DrugLicenceNo2 = model?.DrugLicenceNo2
            };
        }

        private ChemistResponseModel MapToChemistResponse(ChemistMaster entity)
        {
            return new ChemistResponseModel
            {
                Id = entity.Id,
                CompanyName = entity.CompanyName,
                AddressLine1 = entity?.AddressLine1,
                AddressLine2 = entity?.AddressLine2,
                City = entity?.City,
                Pincode = entity?.Pincode,
                MobileNo = entity?.MobileNo,
                GstNo = entity?.GstNo,
                GstIssueDate = entity?.GstIssueDate,
                TinNo = entity?.TinNo,
                TinIssueDate = entity?.TinIssueDate,
                CstNo = entity?.CstNo,
                CstIssueDate = entity?.CstIssueDate,
                DrugLicenceNo1 = entity?.DrugLicenceNo1,
                DrugLicenceNo2 = entity?.DrugLicenceNo2,
                CreatedAt = entity?.CreatedAt,
                ModifiedAt = entity?.ModifiedAt
            };
        }

        #endregion
    }
}
