using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Context
{
    public class ErpDbContextFactory : IDesignTimeDbContextFactory<ErpDbContext>
    {
        public ErpDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=192.168.5.164;Database=erp;User Id=stajkullanici;Password=ikbal2323!;TrustServerCertificate=True;",
                sqlOptions =>
                {
                   
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);

                    sqlOptions.CommandTimeout(60);
                });

            return new ErpDbContext(optionsBuilder.Options);
        }
    }
}

