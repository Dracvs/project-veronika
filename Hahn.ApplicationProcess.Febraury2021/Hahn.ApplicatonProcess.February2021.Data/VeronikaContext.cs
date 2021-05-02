using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public class VeronikaContext : DbContext
    {
        public VeronikaContext(DbContextOptions<VeronikaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if(modelBuilder == null)
            {
                return;
            }

            modelBuilder.Entity<Asset>().ToTable("Asset").HasKey(K => K.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
