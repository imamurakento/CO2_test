using System.Diagnostics;
using CO2.At.Src.aInit;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using Application = Microsoft.Maui.Controls.Application;

namespace CO2;

public partial class App : Application
{
    public App()
    {
        Debug.WriteLine("Application:App:Start");

        InitializeComponent();

        AppManager appManager = new ();
        appManager.Init();
        GIc.SetAppManager(appManager);

        MainPage = new AppShell();
    }


}
