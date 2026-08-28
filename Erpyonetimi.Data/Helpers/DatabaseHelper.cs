using Erpyonetimi.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
namespace Erpyonetimi.Data.Helpers
{
    public class DatabaseHelper
    {
        public static bool IsConnected { get;  set; }
        public static bool IsChecked { get; set; }
        public static void CheckConnection()
        {
            try
            {
               
               string sqlConn=
                  "Server=192.168.5.164;Database=erp;User Id=stajkullanici;Password=ikbal2323!;TrustServerCertificate=True";

               using var conn = new SqlConnection(sqlConn);
               conn.Open();
               IsConnected = true;
            }
            catch
            {
               IsConnected = false;
            }
            finally
            {
                IsChecked = true;
            }
        }
       
    }
}
