using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.Employee;
using CO2.At.Src.aFunc.GoodCurrency;
using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using CO2.At.Src.bInfrastructure.DB;

#if UNIT_TEST
using CO2.At.Src.Tests.Mock;
#endif

namespace CO2.At.Src.bDomain.aLogics;

internal sealed class SimpleFactory
{

    private bool _isDbOpen=false;
    private AppSetting _appSetting;
    private DBAbstract _sqlBase;

    public SimpleFactory()
    {
        ////iniファイルの読み込み
        string relativeIniFilePath = @"..\..\..\..\..\At\Dat\Setting\CO2.ini";
        string basePath = AppContext.BaseDirectory;                             // 実行ファイルのディレクトリを基準に絶対パスを取得
        string iniFilePath = Path.GetFullPath(Path.Combine(basePath, relativeIniFilePath));

        HIniFIle iniCO2 = new(iniFilePath);
        GIc.SetIniFile(iniCO2);

        _appSetting = new(iniCO2);
        GIc.SetAppSetting(_appSetting);

        MonitorSetting monitorSetting = new(iniCO2);
        GIc.SetMonitorSetting(monitorSetting);

        HWindow baseWindow = new(monitorSetting);
        GIc.SetWindow(baseWindow);

        _sqlBase = CreateDB();
        _isDbOpen = _sqlBase.Connect();
        CreateCustomer();
        CreateEmployee();
        CreateCurrencyGood();
        CreateVw1VM();
    }

    private DBAbstract CreateDB()
    {
        DBAbstract? sqlBase;
        if (_appSetting.IsDbUse)
        {
            sqlBase = new SqlServer();
        }
        else
        {
#if UNIT_TEST
            sqlBase = new DBMock();
#else
            sqlBase = new SqlServer();
#endif
        }

        return sqlBase;
    }

    private void CreateCustomer()
    {
        ICustomer iCustomerRepo;

        ////依存性注入+テスト用と本番用の切り替え
        if (_appSetting.IsDbUse)
        {
            if (_isDbOpen)
            {
                iCustomerRepo = new CustomerRepository(_sqlBase);
            }
            else
            {
                iCustomerRepo = new CustomerDBNullObject();
            }
        }
        else
        {
#if UNIT_TEST
            iCustomerRepo = new CmrMock();
#else
            iCustomerRepo = new CustomerRepository(_sqlBase);
#endif
        }

        GIc.SetVmCmr(new(iCustomerRepo));

    }

    private void CreateCurrencyGood()
    {
        IGood? iCyGoodRepo;

        if (_appSetting.IsDbUse)
        {
            if (_isDbOpen)
            {
                iCyGoodRepo = new GoodRepo(_sqlBase);
            }
            else
            {
                iCyGoodRepo = new GoodNullObject();
            }
        }
        else
        {
#if UNIT_TEST
            iCyGoodRepo = new GoodMock();
#else
            iCyGoodRepo = new CyRepo(_sqlBase);
#endif
        }

        GIc.SetVmCy(new(iCyGoodRepo));

    }

    private void CreateEmployee()
    {
        IEmp iEmployeeRepo;

        if (_appSetting.IsDbUse)
        {
            if (_isDbOpen)
            {
                iEmployeeRepo = new EmplRepo(_sqlBase);
            }
            else
            {
                iEmployeeRepo = new EmpDBNullObject();
            }
        }
        else
        {
#if UNIT_TEST
            iEmployeeRepo = new EmpMock();
#else
            iEmployeeRepo = new EmplRepo(_sqlBase);
#endif
        }

        GIc.SetEmpVm(new(iEmployeeRepo));

    }

    private void CreateVw1VM()
    {
        GIc.SetVw1VM(new Vw1VM());
    }


}