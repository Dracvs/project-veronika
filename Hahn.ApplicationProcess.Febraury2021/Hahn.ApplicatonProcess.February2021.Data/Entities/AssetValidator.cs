using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace Hahn.ApplicatonProcess.February2021.Data.Entities
{
    public class AssetValidatorResult : AbstractValidator<Asset>
    {
        
        public AssetValidatorResult()
        {
            RuleFor(asset => asset.AssetName).MinimumLength(5).NotEmpty().NotNull();
            RuleFor(asset => asset.Department).IsInEnum();
            RuleFor(asset => asset.CountryOfDepartment).NotEmpty().NotNull().WithMessage("Country does not exist");
            RuleFor(asset => asset.PurchaseDate).GreaterThan(DateTime.Today.AddYears(-1));
            RuleFor(asset => asset.EmailAddressOfDepartment).EmailAddress();
            RuleFor(asset => asset.Broken).NotNull();
        }
    }
}
