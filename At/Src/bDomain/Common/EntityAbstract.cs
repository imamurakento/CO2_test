using System.Diagnostics;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Helpers;
using Microsoft.Data.SqlClient;

namespace CO2.At.Src.bDomain.Common;

public abstract class EntityAbstract
{
    protected DBAbstract? DbServer { get; set; }

    protected SqlConnection? SqlConnection { get; set; }
    public void CEntityAbstract(DBAbstract baseSql)
    {

        try
        {
            DbServer = baseSql ?? throw new CustomNullException($"{nameof(DbServer)}" + Def.NULLASCCESS);

            SqlConnection = DbServer.Get_SqlConnection();
            ////汎用BaseSQLのクラスからアクセスするように検討（sumi:要修正）
            if (SqlConnection == null)
            {
                throw new CustomNullException($"{nameof(SqlConnection)}" + Def.NULLASCCESS);
            }
        }
        catch (CustomNullException ex)
        {
            Debug.WriteLine("Exception:Msg:" + ex.Message);
            HLog.Except(ex);
        }
    }


}
