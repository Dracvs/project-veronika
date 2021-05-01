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
        Task SaveAsync(Asset asset);
        Task<int> Count();
    }
}
