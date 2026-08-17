using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Erpyonetimi.Context
{
    public class ErpDbContextFactory : IDesignTimeDbContextFactory<ErpDbContext>
    {
        public ErpDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpDbContext>();

            string sqlConn=
                "Server=192.168.5.164;Database=erp;User Id=stajkullanici;Password=ikbal2323!;TrustServerCertificate=True;";

                optionsBuilder.UseSqlServer(sqlConn); 
               
           
           
            return new ErpDbContext(optionsBuilder.Options);
        }
    }
}

