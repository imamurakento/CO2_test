using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;

namespace CO2.At.Src.bDomain.aLogics;

internal static class GIc
{

#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。

    private static AppManager AppMgr { get; set; }
    private static SimpleFactory Factory { get; set; }
    private static HIniFIle IniFile { get; set; }
    private static AppSetting ASetting{ get; set; }
    private static MonitorSetting MSetting { get; set; }
    private static HWindow BaseWindow { get; set; }

    private static CmrVM VmCmr { get; set; }
    private static EmpVM VmEmp { get; set; }

    private static GoodVM VmCy { get; set; }

    private static Vw1VM VmVw1 { get; set; }

#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。

    public static AppManager GetAppManager() => AppMgr;
    public static void SetAppManager(AppManager instance)
    {
        AppMgr = instance;
    }

    public static SimpleFactory GetFactory() => Factory;
    public static void SetFactory(SimpleFactory instance)
    {
        Factory = instance;
    }

    public static HIniFIle GetIniFile() => IniFile;
    public static void SetIniFile(HIniFIle instance)
    {
        IniFile = instance;
    }

    public static AppSetting GetAppSetting() => ASetting;
    public static void SetAppSetting(AppSetting instance)
    {
        ASetting = instance;
    }
    public static MonitorSetting GetMonitorSetting() => MSetting;
    public static void SetMonitorSetting(MonitorSetting instance)
    {
        MSetting = instance;
    }

    public static HWindow GetWindow() => BaseWindow;
    public static void SetWindow(HWindow instance)
    {
        BaseWindow = instance;
    }

    public static CmrVM GetVmCmr() => VmCmr;
    public static void SetVmCmr(CmrVM instance)
    {
        VmCmr = instance;
    }

    public static EmpVM GetEmpVm() => VmEmp;
    public static void SetEmpVm(EmpVM instance)
    {
        VmEmp = instance;
    }

    public static GoodVM GetVmCy() => VmCy;

    public static void SetVmCy(GoodVM instance)
    {
        VmCy = instance;
    }

    public static Vw1VM GetVw1VM() => VmVw1;

    public static void SetVw1VM(Vw1VM instance)
    {
        VmVw1 = instance;
    }

}
