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


        public async Task SaveAsync(Asset asset)
        {            
            await _unitOfWork.AssetRepository.AddAsync(asset);
            await _unitOfWork.SaveAsync();
        }

        public async Task<int> Count()
        {
            var count = await _unitOfWork.AssetRepository.GetAllAsync();
            return count.Count();
        }
    }
}
