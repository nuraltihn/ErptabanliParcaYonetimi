using Microsoft.Data.SqlClient;
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

            string sqlConn=
                "Server=192.168.5.164;Database=erp;User Id=stajkullanici;Password=ikbal2323!;TrustServerCertificate=True;";

           try{
                using var conn = new SqlConnection(sqlConn);
                conn.Open();
                optionsBuilder.UseSqlServer(sqlConn);
            }
            catch
            {
                optionsBuilder.UseSqlite("Data Source=erp.db");
            }
            return new ErpDbContext(optionsBuilder.Options);
        }
    }
}

