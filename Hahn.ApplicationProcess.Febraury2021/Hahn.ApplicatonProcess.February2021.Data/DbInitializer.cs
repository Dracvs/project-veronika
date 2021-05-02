using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public static class DbInitializer
    {
        public static void Initialize(VeronikaContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
