using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
#if UNIT_TEST
using CO2.At.Src.Tests.xUnit;
#endif

namespace CO2.At.Src.bDomain.aLogics;

public class AppManager
{

    private AppState _appState;
    public enum AppState
    {
        PreLogin,
        MainView,
        Customer,
        Employee,
    }

    private readonly Dictionary<AppState, HashSet<AppState>> allowedTransitions = new()
    {
        { AppState.PreLogin, new HashSet<AppState> { AppState.MainView} },
        { AppState.MainView, new HashSet<AppState> { AppState.Customer, AppState.Employee } },
    };

    private bool CanTransit(AppState from, AppState to)
    {
        return allowedTransitions.TryGetValue(from, out var allowedStates) && allowedStates.Contains(to);
    }

    public void SetAppState(AppState state)
    {
        if (CanTransit(_appState, state))
        {
            _appState = state;
        }
        else
        {
            throw new CustomBaseException($"Invalid state transition from {_appState} to {state}");
        }
    }

    public void Init()
    {
        try
        {
            _appState= AppState.PreLogin;

            HLog.InitializeLog();
            HLog.Info("AppManager start");

            // メインのUIスレッドで発生したすべての例外に対するイベントハンドラを追加
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.CurrentDomain_UnhandledException;
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler.OnUnhandledException);                  //// アプリケーションドメイン内のメインUIスレッドを除くすべてのスレッドに対してイベントハンドラを追加

            Assenbly.DispCo2Version();

            ////データベースありの本番用とデータベースがなくても動作するテスト用の切り替え
            SimpleFactory simpleFactory = new();
        }
        catch (Exception ex)
        {
            HLog.Except(ex, "AppManager: Init例外:");
        }

#if UNIT_TEST
        if (GIc.GetAppSetting().IsDebug)
        {

            XUnitTestList xUnitTestList = new ();
            xUnitTestList.TExecute();
        }
#endif

    }

#pragma warning disable IDE0074


}////class