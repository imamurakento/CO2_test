using System;
using CO2.At.Src.aWinForm.aViews;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.Helpers;
using Microsoft.Maui.Controls;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw522_GoodDetai : ContentPage
{
    private double sum;

    public Vw522_GoodDetai()
    {
        InitializeComponent();
    }

    //登録ボタンが押された時の処理
    private void RegisterButtonClicked(object sender, EventArgs e)
    {
        // エントリーから値を取得し足す
        double num1 = double.Parse(productNameEntry.Text);
        double num2 = double.Parse(tradeUnitEntry.Text);
        sum = num1 + num2;


        //和をエントリーに表示
        depositEntry.Text = sum.ToString();

        //和をDisplayAlertに表示
        DisplayAlert("確認", sum.ToString(), "OK");

        // NewContent1 (TestPage3) のウィンドウを開く
    }

    // キャンセルボタンが押された時の処理
    private void CancelButton_Clicked(object sender, EventArgs e)
    { //// 親のウィンドウを取得して閉じる
        //var currentWindow = Application.Current.Windows.FirstOrDefault(w => w.Handler?.PlatformView is Microsoft.Maui.Controls.Handlers.WindowHandler);
        //if (currentWindow != null)
        //{
        //    Application.Current.CloseWindow(currentWindow);
        //}


        //現在のウィンドウを取得して閉じる
        Application.Current?.CloseWindow(GetParentWindow());
    }
}
