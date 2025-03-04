using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.Helpers;
using static CO2.At.Src.bDomain.Helpers.HWindow;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw0 : ContentPage
{
    ////ログイン画面は簡易なので、データバインドを利用せず、コードビハインド実装とする。
    ////データバインドとコードビハインドの実装を比較して、両者のメリット、デメリットを理解すること。
    public Vw0()
    {
        InitializeComponent();
        this.Loaded += Window_Loaded;
        HLog.Info("Vw0:InitializeComponent: OK");

    }

    private void LogInButton_Clicked(object sender, EventArgs e)
    {

        if (GIc.GetEmpVm().ChkLogin(UserNameForm.Text, PasswordForm.Text))
        {
            ErrorText.Text = "ログイン完了";
            //HWindow.CreateWindow(new Vw1(), HWindow.EWindowHierarchyLevel.TopMain);
            GIc.GetWindow().GoToAsync("Vw1");
            //Application.Current?.CloseWindow(GetParentWindow());

        }
        else
        {
            ErrorText.Text = "ユーザー名またはパスワードが間違っています";
        }
    }

    private void Window_Loaded(object sender, EventArgs e)
    {
        try
        {
            var mauiWindow = this.Window.Handler?.PlatformView as MauiWinUIWindow;
            if (mauiWindow == null)
            {
                HLog.Err("vw0 Window_Loaded Window Null Error");
                return;
            }

            HWindow hWindow = GIc.GetWindow();
            hWindow.Get_DisplayInfo();
            hWindow.SetWindowPattern(mauiWindow, WindowStyle.AlwaysOnTopNoResize);

        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }

    }
}