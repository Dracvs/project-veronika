using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly VeronikaContext _context;
        private Repository<int, Asset> _assetRepository;
        private bool disposed = false;

        public UnitOfWork(VeronikaContext context)
        {
            _context = context;
        }

        public Repository<int, Asset> AssetRepository
        {
            get
            {
                if(_assetRepository is null)
                {
                    _assetRepository = new Repository<int, Asset>(_context);
                }
                return _assetRepository;
            }
        }       

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
