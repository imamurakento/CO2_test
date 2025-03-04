using CO2.At.Src.bDomain.Helpers;
using Microsoft.Data.SqlClient;

namespace CO2.At.Src.bDomain.Common;
public abstract class DBAbstract
{
    public DBAbstract()
    {
    }

    public static void DBSqlExcept(Exception ex,string optionMsg="")
    {
        if ((ex is CustomBaseException) || (ex is CustomNullException) || (ex is CustomDbSQLException) || (ex is CustomNoFindException))
        {
            HLog.Err($"カスタム例外発生:種別:{ex.GetType().FullName}");
        }
        else if (ex is SqlException sqlEx)
        {
            HLog.Err(
                        $"SQL実行エラー発生: 内容:{sqlEx.Message} :Number: {sqlEx.Number} LineNumber:{sqlEx.LineNumber} \\n" +
                        $"Source:{sqlEx.Source} :ErrorCode:{sqlEx.ErrorCode} :Procedure:{sqlEx.Procedure} :Data:{sqlEx.Data} \\n" +
                        $"nStackTrace:{sqlEx.StackTrace}");
        }
        HLog.Except(ex, optionMsg);
    }

    public abstract bool Connect();

    public abstract SqlConnection? Get_SqlConnection();


    //ToDo:要単体試験
    public void ConnectionStateOpenAndNullGuard()
    {
        if (this.Get_SqlConnection() == null)
        {
            throw new CustomDbSQLException("DBのコネクションがNULL状態に遷移しています。");
        }
        if (this.Get_SqlConnection()?.State != System.Data.ConnectionState.Open)
        {
            throw new CustomDbSQLException($"DBのコネクションがオープン状態でなくなっています。{this.Get_SqlConnection()?.State}");
        }
    }

}
