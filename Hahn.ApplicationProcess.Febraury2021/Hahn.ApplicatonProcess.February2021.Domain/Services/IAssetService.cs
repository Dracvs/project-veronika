using Hahn.ApplicatonProcess.February2021.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Services
{
    public interface IAssetService
    {
        Task<int> SaveAsync(Asset asset);

        Task<IEnumerable<Asset>> GetAllAssets();

        Task<Asset> FindAsync(int id);

        Task UpdateAsync(Asset asset);

        Task DeleteAsync(int id);

        Task<int> Count();
    }
}
