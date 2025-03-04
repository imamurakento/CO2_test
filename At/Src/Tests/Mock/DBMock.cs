using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Helpers;
using Microsoft.Data.SqlClient;         ////using System.Data.SqlClient;の変わり

#if UNIT_TEST

namespace CO2.At.Src.Tests.Mock;

public class DBMock : DBAbstract
{
    private static readonly SqlConnection? _connection;

    public DBMock()
        : base()
    {
    }

    public override bool Connect()
    {
        HLog.Info("MockDb 仮想接続成功");
        return true;
    }

    public override SqlConnection? Get_SqlConnection()
    {
        return _connection;
    }


}////class

#endif
