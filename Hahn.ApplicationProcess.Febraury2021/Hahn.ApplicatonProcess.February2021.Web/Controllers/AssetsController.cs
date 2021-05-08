using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        [ActionName("GetAsset")]
        [HttpGet("{id}")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsset(int id, [FromServices] IAssetService service)
        {
            var asset = await service.FindAsync(id);            
            return (asset is null) ? NotFound(String.Format("The asset with id {0} was not found.", id)) : Ok(asset);             
        }

        [HttpGet]
        [ActionName("GetAssets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Asset>> GetAssets([FromServices] IAssetService service)
        {
            return await service.GetAllAssets();
        }

        [HttpPut]
        [ActionName("UpdateAsset")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsset(Asset asset, [FromServices]IAssetService service)
        {
            try
            {
                await service.UpdateAsync(asset);
            }
            catch (HttpRequestException exception)
            {
                return BadRequest(exception.Data);
            }
            catch (ArgumentException ex)
            {
                return NotFound(string.Format("The asset with ID '{0}' and name '{1}' was not found.", asset.Id, asset.AssetName));
            }
            

            return Ok();
        }

        [HttpPost]
        [ActionName("CreateAsset")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsset(
            Asset asset,
            [FromServices] IAssetService service)
        {
            try
            {
                await service.SaveAsync(asset);
                return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, asset);
            }
            catch(HttpRequestException exception)
            {
                return BadRequest(exception.Data);                
            }
            
        }

        [HttpDelete]
        [Route("{id}")]
        [ActionName("DeleteAsset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsset(int id, [FromServices] IAssetService service)
        {
            try
            {
                await service.DeleteAsync(id);
            }
            catch (ArgumentException)
            {

                return NotFound(string.Format("The asset with ID '{0}' was not found.", id));
            }

            return Ok();
        }
    }
}
