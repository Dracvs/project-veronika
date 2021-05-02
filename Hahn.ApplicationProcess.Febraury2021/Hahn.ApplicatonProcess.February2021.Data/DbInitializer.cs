using Hahn.ApplicatonProcess.February2021.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public static class DbInitializer
    {        
        public async static void Initialize(VeronikaContext context)
        {
            context.Database.EnsureCreated();

            var asset = new Asset()
            {
                AssetName = "Berlin",
                Broken = false,
                CountryOfDepartment = "Deutschland",
                Department = Department.HQ,
                EmailAddressOfDepartment = "someaddress@someserver.net",
                PurchaseDate = DateTime.Now
            };

            var asset_1 = new Asset()
            {
                AssetName = "Leipzig",
                Broken = false,
                CountryOfDepartment = "Deutschland",
                Department = Department.HQ,
                EmailAddressOfDepartment = "someaddress@someserver.net",
                PurchaseDate = DateTime.Now
            };

            var asset_2 = new Asset()
            {
                AssetName = "Hamburg",
                Broken = false,
                CountryOfDepartment = "Deutschland",
                Department = Department.HQ,
                EmailAddressOfDepartment = "someaddress@someserver.net",
                PurchaseDate = DateTime.Now
            };

            await context.AddRangeAsync(asset, asset_1, asset_2);
            await context.SaveChangesAsync();

            
        }
    }
}
