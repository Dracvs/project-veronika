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
        public async Task<IActionResult> CreateAsset()
        {
            return Ok("Post Action");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsset()
        {
            return Ok("Delete Action");
        }
    }
}
