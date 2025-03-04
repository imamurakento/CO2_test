using System.Diagnostics;
using CO2.At.Src.aWinForm.aViews;
using CO2.At.Src.bDomain.Helpers;

namespace CO2.At.Src.aInit;

public partial class AppShell : Shell
{
    public AppShell()
    {
        Debug.WriteLine("AppShell:Start");

        InitializeComponent();

        //Routing.RegisterRoute(nameof(Vw22), typeof(Vw22));

    }
}
