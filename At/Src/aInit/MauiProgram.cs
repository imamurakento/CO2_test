using System.Diagnostics;
using CO2.At.Src.bDomain.Helpers;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Office.Interop.Excel;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using UraniumUI;
using Windows.Graphics;

namespace CO2.At.Src.aInit;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Debug.WriteLine("MauiProgram:CreateMauiApp:Start");

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
            {
                //fonts.AddFont("Resources/Fonts/OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();
                fonts.AddMaterialSymbolsFonts();
                //fonts.AddFont("msgothic.ttf", "MSGothic");
#if DEBUG
                foreach (var f in fonts)
                {
                    Debug.WriteLine($"Font: {f.Filename} / {f.Alias}");
                }
#endif
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        MauiApp mauiApp = builder.Build();

        return mauiApp;

    }
}
