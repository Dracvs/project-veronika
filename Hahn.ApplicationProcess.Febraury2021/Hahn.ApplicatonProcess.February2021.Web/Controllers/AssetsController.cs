using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Domain.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    [ApiController]    
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {        
        private readonly IAssetService _service;

        public AssetsController(IAssetService service)
        {            
            _service = service;
        }
        
        
        /// <summary>
        /// Get an asset identified by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The asset if it exists</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /Assets/{id}
        ///     {
        ///         "id": 1
        ///     }
        /// </remarks>
        /// <response code="201">Returns the asset</response>
        /// <response code="404">If the asset was not found</response>
        /// <response code="500">if there was an unhandled exception</response>
        [ActionName("GetAsset")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsset(int id)
        {
            var asset = await _service.FindAsync(id);            
            return (asset is null) ? NotFound(String.Format("The asset with id {0} was not found.", id)) : Ok(asset);
        }

        /// <summary>
        /// Get a List of assets
        /// </summary>
        /// <returns>A lists of assets</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /Assets/
        ///     
        /// </remarks>
        /// <response code="200">Gets a list of assets</response>        
        /// <response code="500">if there was an unhandled exception</response>
        [HttpGet]
        [ActionName("GetAssets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<Asset>> GetAssets()
        {
            return await _service.GetAllAssets();
        }

        /// <summary>
        /// Updates an asset
        /// </summary>
        /// <param name="asset">A class representing the asset</param>
        /// <returns>OK if successful</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /Assets/
        ///     {
        ///         "assetName": "Muenchen",
        ///         "department": 0,
        ///         "countryOfDepartment": "Germany",
        ///         "emailAddressOfDepartment": "someaddress@someserver.net",
        ///         "purchaseDate": "2021-05-11T20:08:06.0081303-05:00",
        ///         "broken": false
        ///      }
        /// </remarks>
        /// <response code="201">Returns the asset</response>
        /// <response code="404">If the asset was not found</response>
        /// <response code="500">If there was an unhandled error</response>
        [HttpPut]
        [ActionName("UpdateAsset")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsset(Asset asset)
        {
            try
            {
                await _service.UpdateAsync(asset);
            }
            catch (HttpRequestException exception)
            {                
                Log.Error("The asset didnt pass validation", exception.Data);
                return BadRequest(exception.Data);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, string.Format("The asset with ID '{0}' and name '{1}' was not found.", asset.Id, asset.AssetName));
                return NotFound(string.Format("The asset with ID '{0}' and name '{1}' was not found.", asset.Id, asset.AssetName));
            }
            

            return Ok();
        }

        /// <summary>
        /// Creates an asset
        /// </summary>
        /// <param name="asset">A class representing the asset</param>
        /// <returns>The Asset created</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Assets/
        ///     {
        ///         "assetName": "Muenchen",
        ///         "department": 0,
        ///         "countryOfDepartment": "Germany",
        ///         "emailAddressOfDepartment": "someaddress@someserver.net",
        ///         "purchaseDate": "2021-05-11T20:08:06.0081303-05:00",
        ///         "broken": false
        ///      }
        /// </remarks>
        /// <response code="201">Returns the asset</response>
        /// <response code="422">If the asset could not be created</response>
        /// <response code="500">If there was an unhandled error</response>
        [HttpPost]
        [ActionName("CreateAsset")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsset(Asset asset)
        {
            try
            {
                await _service.SaveAsync(asset);
                return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, asset);
            }
            catch(HttpRequestException exception)
            {
                Log.Error("Asset creation failed", exception.Data);                
                return BadRequest(exception.Data);                
            }
            
        }


        /// <summary>
        /// Deletes an asset
        /// </summary>
        /// <param name="asset">An intger ID</param>
        /// <returns>Ok if successful</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /Assets/{id}
        ///     {   
        ///         "id" : 1
        ///      }
        /// </remarks>
        /// <response code="201">Returns ok if successful</response>
        /// <response code="404">If the asset could not be found</response>
        /// <response code="500">If there was an unhandled error</response>
        [HttpDelete]
        [Route("{id}")]
        [ActionName("DeleteAsset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, string.Format("The asset with ID '{0}' was not found.", id));
                return NotFound(string.Format("The asset with ID '{0}' was not found.", id));
            }

            return Ok();
        }
    }
}
