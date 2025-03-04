using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Helpers;
using Microsoft.Data.SqlClient;

namespace CO2.At.Src.bInfrastructure.DB;

public class SqlServer : DBAbstract
{
    private string? _connectionString;
    private SqlConnection? _connection;

    public SqlServer()
        : base()
    {
    }

    public override bool Connect()
    {
        try
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = @"localhost",
                InitialCatalog = "CO2V1",
                IntegratedSecurity = true,
                Encrypt = false, //// SSL暗号化を無効にする
                TrustServerCertificate = true, //// サーバー証明書の検証をスキップする
            };
            _connectionString = builder.ToString();

            _connection = new SqlConnection(_connectionString);
            //_connection.OpenAsync().Wait();
            //_connection.OpenAsync();
            _connection.Open();

            HLog.Info("SQL Server接続成功");

            return true;
        }
        catch (Exception ex)
        {
            HLog.Err($"SQL Server接続失敗: statusMessage:{ex.Message}");
            HLog.Except(ex);
            return false;
        }
    }

    public override SqlConnection? Get_SqlConnection() => _connection;

}////class
