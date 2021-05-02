using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Services
{
    public class AssetService : IAssetService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> SaveAsync(Asset asset)
        {            
            await _unitOfWork.AssetRepository.AddAsync(asset);
            await _unitOfWork.SaveAsync();
            return asset.Id;
        }

        public async Task<IEnumerable<Asset>> GetAllAssets()
        {
            return await _unitOfWork.AssetRepository.GetAllAsync(orderBy: q => q.OrderBy(d => d.Id));
        }

        public async Task<Asset> FindAsync(int id)
        {
            return await _unitOfWork.AssetRepository.FindAsync(id);
        }

        public async Task UpdateAsync(Asset asset)
        {
            var entity = await  _unitOfWork.AssetRepository.FindAsync(asset.Id);
            if(entity != null)
            {
                entity.AssetName = asset.AssetName;
                entity.Broken = asset.Broken;
                entity.CountryOfDepartment = asset.CountryOfDepartment;
                entity.Department = asset.Department;
                entity.EmailAddressOfDepartment = asset.EmailAddressOfDepartment;
                entity.PurchaseDate = asset.PurchaseDate;
                
                _unitOfWork.AssetRepository.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new ArgumentException(string.Format("The Asset with id {0} was not found", asset.Id));
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.AssetRepository.FindAsync(id);
            if(entity != null)
            {
                _unitOfWork.AssetRepository.Delete(entity);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new ArgumentException(string.Format("The Asset with id {0} was not found", id));
            }
        }

        public async Task<int> Count()
        {
            var count = await _unitOfWork.AssetRepository.GetAllAsync();
            return count.Count();
        }
    }
}
