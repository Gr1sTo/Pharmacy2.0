using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.src
{
   using Microsoft.EntityFrameworkCore;
   using Microsoft.EntityFrameworkCore.Design;
   using Microsoft.Extensions.Configuration;

   public class PharmacyContextFactory : IDesignTimeDbContextFactory<PharmacyContext>
   {
        public PharmacyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PharmacyContext>();
            optionsBuilder.UseSqlServer("PharmacyDatabase");

            return new PharmacyContext(optionsBuilder.Options);
        }
   }

}