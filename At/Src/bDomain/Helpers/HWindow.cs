using System.Runtime.InteropServices;
using CO2.At.Src.aWinForm.aViews;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using Xunit;

namespace CO2.At.Src.bDomain.Helpers;

internal class HWindow
{

    public enum WindowStyle
    {
        AlwaysOnTopResize,
        AlwaysOnTopNoResize,
        NoAlwaysOnTopResize,
        NoAlwaysOnTopNoResize,
    }

    private readonly int _screenWidth;
    private readonly int _screenHeight;

    private MonitorSetting _monitorSetting;

    private int _realWidth=0;
    private int _realHeight=0;
    private int _realDestiny=1;

    public HWindow(MonitorSetting monitorSetting)
    {
        _monitorSetting = monitorSetting;
        _screenWidth = _monitorSetting.ResolutionWidth;
        _screenHeight = _monitorSetting.ResolutionHeight;
    }


    public async void GoToAsync(ShellNavigationState state,bool isAddRoutePath=true)
    {

        try
        {
            string pageName = state.Location.OriginalString;
            if (MainThread.IsMainThread == false)
            {
                HLog.Warn("GoToAsync:Change MainThread");
            }

            StopWatch hStop = new($"処理時間計測デバッグ:GoToAsync state:{pageName}");
            hStop.Start();

            if (GIc.GetAppSetting().IsShellMode)
            {
                ShellNavigationState shellNavigationState;
                if (isAddRoutePath)
                {
                    pageName = $"//{state.Location.OriginalString}";
                    shellNavigationState = pageName;
                }
                else
                {
                    shellNavigationState = state;
                }

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Shell.Current.GoToAsync(shellNavigationState);
                });
            }
            hStop.StopAndShowTime();
        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }
    }




#if WINDOWS

    public Application GetApplication()
    {
        return Application.Current;
    }

    public Window? GetCurrentWindow()
    {
        var window = GetApplication().Windows.FirstOrDefault();
        if (window == null)
        {
            HLog.Err("getCurrentWindow Window Null Error");
            return null;
        }

        HLog.Info("GetCurrentWindow: name: " + window.ToString());

        return window;
    }
    public AppWindow GetAppWindow(MauiWinUIWindow window)
    {
        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        Microsoft.UI.WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        return AppWindow.GetFromWindowId(windowId);
    }

    //public AppWindow GetAppWindowFromHandle(nint windowHandle)
    //{
    //    Microsoft.UI.WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
    //    return AppWindow.GetFromWindowId(windowId);
    //}

    public void Get_DisplayInfo()
    {

        //// https:////developers-trash.com/archives/974
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            //// ディスプレイ解像度の取得
            _realDestiny = (int)DeviceDisplay.MainDisplayInfo.Density;
            _realWidth = (int)(DeviceDisplay.MainDisplayInfo.Width / _realDestiny);
            _realHeight = (int)(DeviceDisplay.MainDisplayInfo.Height / _realDestiny);

            HLog.Info("Window Get_DisplayInfo : Screen Size mainScreen.width:" + _realWidth + " MainScreenHeight;" + _realHeight + " Destiny:" + _realDestiny);
        });
    }

    private ContentPageSize CalcContentPageSize(double widthRate, double heightRate)
    {
        ContentPageSize pageSize;

        pageSize = new ContentPageSize((int)(_screenWidth * widthRate), (int)(_screenHeight * heightRate));
        return pageSize;
    }

    private Coordinate CalcCenterCoordinate(ContentPageSize contentPageSize)
    {
        int widthTmp;
        int heightTmp;

        int startXTmp = _screenWidth - contentPageSize.Width;
        if(startXTmp == 0)
        {
            widthTmp = 0;
        }
        else
        {
            widthTmp = (int)startXTmp / 2;
        }

        int startYTmp = _screenHeight - contentPageSize.Height;
        if (startYTmp == 0)
        {
            heightTmp = 0;
        }
        else
        {
            heightTmp = (int)startYTmp / 2;
        }

        return new Coordinate(widthTmp, heightTmp);
    }

    private void SetWindowSize(Window window, ContentPageSize pageSize, Coordinate pagePos,bool isMinimun=false)
    {
        window.Width = pageSize.Width;
        window.Height = pageSize.Height;
        window.X = pagePos.X;
        window.Y = pagePos.Y;

        if(isMinimun)
        {
            window.MinimumWidth = pageSize.Width;
            window.MinimumHeight = pageSize.Height;
        }
    }

    private void OpenWindows(Window window)
    {
        if (Application.Current != null)
        {
            Application.Current.OpenWindow(window);
        }
        else
        {
            HLog.Err("Error: Current is null.:");
        }
    }


    public void CreateWindow(ContentPage multi_page, double widthRate, double heightRate, bool isMinimun = false)
    {
        try
        {
            ContentPageSize pageSize = CalcContentPageSize(widthRate, heightRate);
            Coordinate centerPos = CalcCenterCoordinate(pageSize);

            var window = new Window(multi_page);

            SetWindowSize(window, pageSize, centerPos, isMinimun);
            OpenWindows(window);

            HLog.Info($"CreateWindowRate: name: {multi_page.ToString()} width: {pageSize.Width} Height: {pageSize.Height}");
        }
        catch (Exception ex)
        {
            HLog.Err("Exception:Msg:" + ex.Message);
        }
    }

    public void WindowResizeMove(MauiWinUIWindow maui_window, ContentPageSize pageSize, Coordinate pagePos)
    {
        try
        {
            AppWindow appWindow = GetAppWindow(maui_window) ?? throw new InvalidOperationException("NULL ACCESS ERROR");

            appWindow.Resize(new SizeInt32(pageSize.Width, pageSize.Height));
            appWindow.Move(new PointInt32(pagePos.X, pagePos.Y));

            HLog.Info($"WindowResizeMove:width: {pageSize.Width} Height: {pageSize.Height}");
        }
        catch (Exception ex)
        {
            HLog.Err("Exception:Msg:" + ex.Message);
        }
    }


    public void ChangeMaximize(AppWindow appWindow,bool isMaximize)
    {
        try
        {
            OverlappedPresenter overlappedPresenterTmp = appWindow.Presenter as OverlappedPresenter ?? throw new CustomNullException("overlappedPresenter NULL ACCESS ERROR");
            HLog.Info($"overlappedPresenter: {overlappedPresenterTmp} Kind: {overlappedPresenterTmp.Kind}");

            switch (appWindow.Presenter)
            {
                case OverlappedPresenter overlappedPresenter:

                    if (!isMaximize)
                    {
                        ////appWindow.SetPresenter(AppWindowPresenterKind.CompactOverlay); Shell遷移ではその他のWindowも影響を受ける
                        //appWindow.Resize(new SizeInt32(1280, 1000));
                        overlappedPresenter.SetBorderAndTitleBar(true, true);
                        overlappedPresenter.Restore();
                    }
                    else
                    {
                        overlappedPresenter.SetBorderAndTitleBar(true, true);
                        overlappedPresenter.Maximize();
                        ////ShowWindow(window.WindowHandle, 3);                  ////最大化するAPI(使用せずとも最大化可能）
                        //appWindow.SetPresenter(AppWindowPresenterKind.Overlapped);
                    }

                    break;
            }
        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }
    }



    //// https://tera1707.com/entry/2022/04/24/220519
    public void SetWindowPattern(MauiWinUIWindow maui_window, WindowStyle windowStyle)
    {
        try
        {
            AppWindow appWindow = GetAppWindow(maui_window) ?? throw new CustomNullException("GetAppWindow NULL ACCESS ERROR");

            var op = OverlappedPresenter.Create();                                  //// 標準タイプのウインドウ(OverlappedPresenter)の取得

            ////ウィンドウ階層に応じてウィンドウのスタイルを変更する
            /////
            switch (windowStyle)
            {
                case WindowStyle.NoAlwaysOnTopResize:
                    op.IsMaximizable = true;
                    op.IsMinimizable = true;
                    op.IsResizable = true;
                    op.IsAlwaysOnTop = false;
                    break;
                case WindowStyle.NoAlwaysOnTopNoResize:
                    op.IsMaximizable = false;
                    op.IsMinimizable = false;
                    op.IsResizable = false;
                    op.IsAlwaysOnTop = false;
                    break;
                case WindowStyle.AlwaysOnTopResize:
                    op.IsMaximizable = true;
                    op.IsMinimizable = true;
                    op.IsResizable = true;
                    op.IsAlwaysOnTop = true;
                    break;
                case WindowStyle.AlwaysOnTopNoResize:
                    op.IsMaximizable = false;
                    op.IsMinimizable = false;
                    op.IsResizable = false;
                    op.IsAlwaysOnTop = true;
                    break;
            }
            appWindow.SetPresenter(op);

        }
        catch (Exception ex)
        {
            HLog.Err("Exception:Msg:" + ex.Message);
        }
    }

    public async Task toastPush(string txt)
    {

        if (MainThread.IsMainThread)
        {
            HLog.Warn("Change toastPush");
        }

        CancellationTokenSource cancellation = new CancellationTokenSource();
        ToastDuration dur = ToastDuration.Long;

        double fontsize = 25;
        var toast = Toast.Make(txt, dur, fontsize);
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await toast.Show(cancellation.Token);
        });
    }

    //public void CreateWindowSize(ContentPage multi_page, int width, int height)
    //{
    //    try
    //    {
    //        int startXTmp = (ScreenWidth - width) / 2;
    //        int startYTmp = (ScreenHeight - height) / 2;

    //        var window = new Window(multi_page);

    //        window.Width = width;
    //        window.Height = height;
    //        window.X = startXTmp;
    //        window.Y = startYTmp;

    //        window.MinimumWidth = width;
    //        window.MinimumHeight = height;
    //        window.MaximumWidth = width;
    //        window.MaximumHeight = height;

    //        if (Application.Current != null)
    //        {
    //            Application.Current.OpenWindow(window);
    //        }
    //        else
    //        {
    //            HLog.Err("Error: Current is null.:");
    //        }

    //        HLog.Info("CreateWindow:  name: " + multi_page.ToString() + " width:" + width + " Height:" + height + " startXTmp:" + startXTmp + " startYTmp: " + startYTmp);
    //    }
    //    catch (Exception ex)
    //    {
    //        HLog.Err("Exception:Msg:" + ex.Message);
    //    }
    //}

    ////サポート警告がでるためコメントアウト
    //#if WINDOWS
    //    [DllImport("user32.dll")]
    //    public static extern bool ShowWindow(nint hWnd, int cmdShow);
    //#endif

    //public static void Get_CurrentDisplayInfo()
    //{
    //    int destiny;

    //    var disp = DeviceDisplay.Current.MainDisplayInfo;
    //    destiny = (int)disp.Density;
    //    var currentWidth = disp.Width / destiny;
    //    var currentHeight = disp.Height / destiny;

    //    ////現在のディスプレイと記憶しているディスプレイが変化するかのチェック（マルチディスプレイ対策)
    //    if (currentWidth != ScreenWidth || currentHeight != ScreenHeight)
    //    {
    //        HLog.Info("Window Get_DisplayInfo : DisplayScreenSizeChanged");
    //        ScreenWidth = (int)currentWidth;
    //        ScreenHeight = (int)currentHeight;
    //    }

    //    HLog.Info("Window Get_DisplayInfo : Screen Size mainScreen.width:" + ScreenWidth + " MainScreenHeight;" + ScreenHeight + " Destiny:" + destiny);
    //}

    //public void CreateWindow(string pageName, double widthRate, double heightRate, bool isMinimun = false)
    //{
    //    try
    //    {
    //        ContentPage? pageInstance = null;
    //        Type? pageType = Type.GetType($"CO2.At.Src.aWinForm.aViews.{pageName}");        // 名前空間を含めた型名を取得
    //        if (pageType != null)
    //        {
    //            pageInstance = Activator.CreateInstance(pageType) as ContentPage;     // インスタンスの生成
    //        }

    //        if (pageInstance == null)
    //        {
    //            HLog.Err($"Error: {pageName} の型が見つからない、または ContentPage にキャストできません。");
    //            return;
    //        }

    //        ContentPageSize pageSize = CalcContentPageSize(widthRate, heightRate);
    //        Coordinate centerPos = CalcCenterCoordinate(pageSize);

    //        Window window = new(pageInstance);

    //        SetWindowSize(window, pageSize, centerPos, isMinimun);
    //        OpenWindows(window);

    //        HLog.Info($"CreateWindow: name: {pageName} width: {pageSize.Width} Height: {pageSize.Height}");
    //    }
    //    catch (Exception ex)
    //    {
    //        HLog.Err("Exception:Msg:" + ex.Message);
    //    }
    //}



#endif              //// #if WINDOWS

}////class