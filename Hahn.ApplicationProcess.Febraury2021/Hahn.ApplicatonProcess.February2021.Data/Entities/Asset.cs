using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Entities
{
    /// <summary>
    /// Asset entity
    /// </summary>
    public class Asset : BaseEntity<int>
    {
        /// <summary>
        /// The name of the asset. 5 characters minimum
        /// </summary>
        [DefaultValue("")]
        public string AssetName { get; set; }

        /// <summary>
        /// The Department the asset belongs to.
        /// </summary>
        [DefaultValue(0)]
        public Department Department { get; set; }

        /// <summary>
        /// The Country assigned
        /// </summary>
        [DefaultValue("Germany")]
        public string CountryOfDepartment { get; set; }

        /// <summary>
        /// A valid email address
        /// </summary>
        [DefaultValue("My@Email.com")]
        public string EmailAddressOfDepartment { get; set; }

        /// <summary>
        /// The date of purchase
        /// </summary>
        [DefaultValue("2021-05-11")]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Whether the asset is broken
        /// </summary>
        [DefaultValue(false)]
        public bool Broken { get; set; }
    }

    public enum  Department
    {
        HQ,
        Store1,
        Store2,
        Store3,
        MaintenanceStation
    }
}
