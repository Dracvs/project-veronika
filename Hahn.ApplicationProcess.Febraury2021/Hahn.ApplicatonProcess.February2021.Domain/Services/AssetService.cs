using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using FluentValidation.Results;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Hahn.ApplicatonProcess.February2021.Domain.Services
{
    public class AssetService : IAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AssetService> _logger;
        private readonly IConfiguration _configuration;

        public AssetService(IUnitOfWork unitOfWork, ILogger<AssetService> logger, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<int> SaveAsync(Asset asset)
        {
            var assetValidation = await  IsAssetValid(asset);
            if(assetValidation.IsValid)
            {
                await _unitOfWork.AssetRepository.AddAsync(asset);
                await _unitOfWork.SaveAsync();                
            }
            else
            {
                _logger.LogError(string.Format("Creation failed: The Asset failed validation (Asset name: {0})", asset.AssetName));
                throw BuildException(assetValidation);
            }

            return asset.Id;
        }

        public async Task<IEnumerable<Asset>> GetAllAssets()
        {
            return await _unitOfWork.AssetRepository.GetAllAsync(orderBy: q => q.OrderBy(d => d.Id));
        }

        public async Task<Asset> FindAsync(int id)
        {
            return await _unitOfWork.AssetRepository.FindAsync(id);
        }

        public async Task UpdateAsync(Asset asset)
        {
            var entity = await  _unitOfWork.AssetRepository.FindAsync(asset.Id);
            if(entity != null)
            {
                var assetValidation = await IsAssetValid(asset);
                if(assetValidation.IsValid)
                {
                    entity.AssetName = asset.AssetName;
                    entity.Broken = asset.Broken;
                    entity.CountryOfDepartment = asset.CountryOfDepartment;
                    entity.Department = asset.Department;
                    entity.EmailAddressOfDepartment = asset.EmailAddressOfDepartment;
                    entity.PurchaseDate = asset.PurchaseDate;

                    _unitOfWork.AssetRepository.Update(entity);
                    await _unitOfWork.SaveAsync();
                }
                else
                {
                    _logger.LogError(string.Format("Update failed: The Asset with id {0} failed validation", asset.Id));
                    throw BuildException(assetValidation);
                }
                
            }
            else
            {
                _logger.LogError(string.Format("Update failed: The Asset with id {0} was not found", asset.Id));
                throw new ArgumentException(string.Format("The Asset with id {0} was not found", asset.Id));
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.AssetRepository.FindAsync(id);
            if(entity != null)
            {
                _unitOfWork.AssetRepository.Delete(entity);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                _logger.LogError(string.Format("Delete failed: The Asset with id {0} was not found", id));
                throw new ArgumentException(string.Format("The Asset with id {0} was not found", id));
            }
        }

        public async Task<int> Count()
        {
            var count = await _unitOfWork.AssetRepository.GetAllAsync();
            return count.Count();
        }

        private async Task<ValidationResult> IsAssetValid(Asset asset)
        {
            
            var validation = new AssetValidatorResult().Validate(asset);
            var doesCountryExist = await DoesCountryExist(asset.CountryOfDepartment);
            if(!doesCountryExist)
            {
                var validationFailuere = new ValidationFailure("CountryOfDepartment", "Country does not exist") 
                {
                    AttemptedValue = "CountryOfDepartment",
                    ErrorCode = "EqualValidator",
                    ErrorMessage = "Country does not exist",
                    PropertyName = "CountryOfDepartment"
                };
                validation.Errors.Add(validationFailuere);
                _logger.LogError(String.Format("The country {0} doesn't exist", asset.CountryOfDepartment));
            }
            return validation;
        }

        private async Task<bool> DoesCountryExist(string name)
        {
            using HttpClient client = new()
            {
                BaseAddress = new Uri(_configuration.GetSection("API")["RESTCountries"])
            };

            try
            {
                var result = await client.GetFromJsonAsync<JsonElement>(name);
                return result[0].GetProperty("name").GetString().Equals(name);                    
            }
            catch(Exception)
            {                
                return false;
            }            
        }

        private HttpRequestException BuildException(ValidationResult assetValidation)
        {
            var exception = new HttpRequestException("Model validation failed", null, statusCode: HttpStatusCode.BadRequest);

            foreach (var item in assetValidation.Errors)
            {
                exception.Data.Add(item.ErrorCode, item.ErrorMessage);
            }
            return exception;
        }
    }
}
