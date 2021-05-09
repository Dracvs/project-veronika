using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Entities
{
    public class BaseEntity<TId> where TId : struct
    {
        /// <summary>
        /// The idenfication
        /// </summary>
        public TId Id { get; set; }
    }
}
