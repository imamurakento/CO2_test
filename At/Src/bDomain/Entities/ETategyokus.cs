using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using CO2.At.Src.bInfrastructure.DB;
using CO2.At.Src.Tests.Mock;
using Microsoft.Data.SqlClient;

namespace CO2.At.Src.bDomain.Entities;

public sealed class ETategyokus
{
    private readonly DBAbstract? _sqlBase;
    private ObservableCollection<ETategyoku>? _tategyokuList;

    public ETategyokus()
    {
        if (GIc.GetAppSetting().IsDbUse)
        {
            _sqlBase = new SqlServer();
        }
        else
        {
#if UNIT_TEST
            _sqlBase = new DBMock();
#else
            _sqlBase = new SqlServer();
#endif
        }

        bool isConnect = _sqlBase.Connect();

        _tategyokuList = null;
        AddList();
    }

    public ObservableCollection<ETategyoku>? TategyokuList
    {
        get { return _tategyokuList; }
        set { _tategyokuList = value; }
    }

    public void AddList()
    {
        _tategyokuList = new ObservableCollection<ETategyoku>();

        if (GIc.GetAppSetting().IsDbUse)
        {

            string strSQLTmp = @"select Transaction_ID,Product_Name,Buy_Sell_Category,NumberOfSheets,EstablishmentTime,EstablishmentPrice,StandardPrice,ExchangeRate,MarktoMarketProfitAndLoss,MarktoMarketDeductionAmount from View_OpenInterests_V1";

            try
            {

                using (SqlCommand cmd = new (strSQLTmp, _sqlBase.Get_SqlConnection()))
                {
                    SqlDataReader readerTmp = cmd.ExecuteReader();
                    if (readerTmp == null)
                    {
                        throw new InvalidOperationException("NULL ACCESS ERROR");
                    }

                    while (readerTmp.Read())
                    {
                        ETategyoku tategyoku = new ();

                        tategyoku.Set(
                                         Convert.ToUInt32(readerTmp["Transaction_ID"]),
                                         Convert.ToString(readerTmp["Product_Name"]) ?? throw new InvalidOperationException("Select Null Parameter Err"),
                                         Convert.ToString(readerTmp["Buy_Sell_Category"]) ?? throw new InvalidOperationException("Select Null Parameter Err"),
                                         Convert.ToUInt32(readerTmp["NumberOfSheets"]),
                                         Convert.ToDateTime(readerTmp["EstablishmentTime"]),
                                         Convert.ToDecimal(readerTmp["EstablishmentPrice"]),
                                         Convert.ToDecimal(readerTmp["StandardPrice"]),
                                         Convert.ToDecimal(readerTmp["ExchangeRate"]),
                                         Convert.ToDecimal(readerTmp["MarktoMarketProfitAndLoss"]),
                                         Convert.ToDecimal(readerTmp["MarktoMarketDeductionAmount"]));
                        _tategyokuList.Add(tategyoku);
                    }

                    readerTmp.Close();
                }
            }
            catch (Exception ex)
            {
                HLog.Except(ex);
            }
        }
        else
        {
            ETategyoku tategyoku;

            for (uint i = 0; i < 50; i++)
            {
                tategyoku = new ETategyoku();
                tategyoku.Set(1000 + i, "キャップアンドトレード", "売", 11, new DateTime(2022, 3, 11, 14, 30, 0), 80000 + i, 700000 + i, 11424, 5010, 6010);
                _tategyokuList.Add(tategyoku);
            }
        }
    }
}////class
