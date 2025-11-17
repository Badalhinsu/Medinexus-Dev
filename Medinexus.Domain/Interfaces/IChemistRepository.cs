using Medinexus.Domain.Entities;

namespace Medinexus.Domain.Interfaces
{
    public interface IChemistRepository
    {
        #region ChemistMaster Operations

        Task<ChemistMaster> AddNewChemistAsync(ChemistMaster chemistMaster);
        Task<ChemistMaster?> GetChemistByIdAsync(int id);
        Task<ChemistMaster?> UpdateChemistAsync(ChemistMaster chemistMaster);
        Task<bool> DeleteChemistAsync(int id);

        #endregion
    }
}
