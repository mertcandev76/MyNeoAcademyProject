using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Context
{
    public class MyNeoAcademyContextFactory : IDesignTimeDbContextFactory<MyNeoAcademyContext>
    {
        public MyNeoAcademyContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("SqlConnection");

            var optionsBuilder = new DbContextOptionsBuilder<MyNeoAcademyContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MyNeoAcademyContext(optionsBuilder.Options);
        }
    }
}
