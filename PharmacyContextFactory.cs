using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class PharmacyContextFactory : IDesignTimeDbContextFactory<PharmacyContext>
    {
        public PharmacyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PharmacyContext>();

            //Отримуємо конфігурацію з appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            //Отримуємо строку підключення з appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new PharmacyContext(optionsBuilder.Options);
        }
    }
}