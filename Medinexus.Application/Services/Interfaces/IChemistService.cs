using Medinexus.Application.Models.Chemist;

namespace Medinexus.Application.Services.Interfaces
{
    public interface IChemistService
    {
        #region Chemist Operations

        Task<ChemistResponseModel?> AddNewChemistDetailAsync(ChemistRequestModel chemistRequest);
        Task<ChemistResponseModel?> GetChemistDetailByIdAsync(int chemistId);
        Task<ChemistResponseModel?> UpdateChemistDetailAsync(ChemistRequestModel chemistRequest);
        Task<bool> DeleteChemistDetailAsync(int chemistId);

        #endregion
    }
}
