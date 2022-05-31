using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DBConnectorLib
{
    public enum DataProvider
    {
        SQL,
        OLE_DB
    }
    public class DBConnector
    {
        private string provider = "Provider = SQLOLEDB;";
        public async Task<string> ConnectAndGetVersionAsync(DataProvider dataProvider, string connectionString) 
        {
            //"Server=.;Database=Northwind;Integrated Security = SSPI"
            if (dataProvider == DataProvider.SQL)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    return connection.ServerVersion;
                }
            }
            else
            {
                using (OleDbConnection connection = new OleDbConnection(provider + connectionString))
                {
                    await connection.OpenAsync();
                    return connection.ServerVersion;
                }
            }
        }

    }
}
