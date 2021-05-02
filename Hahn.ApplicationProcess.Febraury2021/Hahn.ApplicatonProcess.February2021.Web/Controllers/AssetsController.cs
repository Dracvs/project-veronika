using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAssetAsync(Guid id)
        {
            return Ok("Get Asset Action");
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAssets()
        {
            return Ok("Get All Assets Action");
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAsset()
        {
            return Ok("Put Action");
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateAsset([FromServices] IAssetService service)
        {
            var asset = new Asset
            {                
                AssetName = "Berliner",
                Broken = false,
                CountryOfDepartment = "Deutschland",
                Department = Department.Store1,
                EmailAddressOfDepartment = "dep@gmail.com",
                PurchaseDate = DateTime.Today            
            };
            await service.SaveAsync(asset);
            var count = await service.Count();
            return Ok(string.Format("The number of items in the Database is: {0}", count));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsset()
        {
            return Ok("Delete Action");
        }
    }
}
