using Medinexus.Domain.Entities;
using Medinexus.Domain.Interfaces;
using Medinexus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Medinexus.Infrastructure.Repositories
{
    public class ChemistRepository : IChemistRepository
    {
        private readonly AppDbContext _appDbContext;

        public ChemistRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region ChemistMaster Methods

        public async Task<ChemistMaster> AddNewChemistAsync(ChemistMaster chemistMaster)
        {
            try
            {
                await _appDbContext.ChemistMasters.AddAsync(chemistMaster);
                await _appDbContext.SaveChangesAsync();
                return chemistMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ChemistMaster?> GetChemistByIdAsync(int id)
        {
            try
            {
                return await _appDbContext.ChemistMasters
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ChemistMaster?> UpdateChemistAsync(ChemistMaster chemistMaster)
        {
            try
            {
                ChemistMaster? existingChemist = await _appDbContext.ChemistMasters
                                                        .FirstOrDefaultAsync(x => x.Id == chemistMaster.Id);
                if (existingChemist == null)
                    return null;

                existingChemist.CompanyName = chemistMaster.CompanyName;
                existingChemist.AddressLine1 = chemistMaster.AddressLine1;
                existingChemist.AddressLine2 = chemistMaster.AddressLine2;
                existingChemist.City = chemistMaster.City;
                existingChemist.Pincode = chemistMaster.Pincode;
                existingChemist.MobileNo = chemistMaster.MobileNo;
                existingChemist.GstNo = chemistMaster.GstNo;
                existingChemist.GstIssueDate = chemistMaster.GstIssueDate;
                existingChemist.TinNo = chemistMaster.TinNo;
                existingChemist.TinIssueDate = chemistMaster.TinIssueDate;
                existingChemist.CstNo = chemistMaster.CstNo;
                existingChemist.CstIssueDate = chemistMaster.CstIssueDate;
                existingChemist.DrugLicenceNo1 = chemistMaster.DrugLicenceNo1;
                existingChemist.DrugLicenceNo2 = chemistMaster.DrugLicenceNo2;
                existingChemist.ModifiedAt = chemistMaster.ModifiedAt;

                //_appDbContext.ChemistMasters.Update(chemistMaster);
                await _appDbContext.SaveChangesAsync();

                return chemistMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteChemistAsync(int id)
        {
            try
            {
                ChemistMaster? existingChemist = await _appDbContext.ChemistMasters
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (existingChemist == null)
                    return false;

                _appDbContext.ChemistMasters.Remove(existingChemist);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}