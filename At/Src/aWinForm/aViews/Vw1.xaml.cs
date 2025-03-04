using System.Collections.ObjectModel;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.ViewTest;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.Helpers;

////using Microsoft.Office.Interop.Excel;
using static CO2.At.Src.bDomain.Helpers.CursorIconHelper;
using static CO2.At.Src.bDomain.Helpers.HWindow;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace CO2.At.Src.aWinForm.aViews;


public partial class Vw1 : ContentPage
{
    private readonly HWindow _hWindow;

    public Vw1()
    {

        try
        {

            //StopWatch hStop = new ("処理時間計測デバッグ:Vw1:コンストラクタ");
            //hStop.Start();

            InitializeComponent();

            HLog.Info("Vw1:InitializeComponent: OK");

            _hWindow = GIc.GetWindow();

            ////AkgulGrid定型処理
            Color rowColorBase = HRreources.GetColor("ColorBase");
            RowPalette.Add(rowColorBase);


            //if (Content is Layout layout)
            //{
            //    RegisterEvent2(layout);
            //}


            RegisterEvent();

            //Vw1Grid.ItemsSource = TategyokuList;
            this.BindingContext = GIc.GetVw1VM();

            //UIにある全てのボタン要素を取得しホバー時にハンドにする
            Dispatcher.Dispatch(() =>
            {
                if (this.Handler?.MauiContext is IMauiContext mauiContext)
                {
                    ApplyCursorToAllButtons(this.Content as VisualElement, CursorIcon.Hand, mauiContext);
                    ApplyAnimationToAllButtons(this.Content as VisualElement);
                }
            });

            //hStop.StopAndShowTime();
        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }
    }
    private async void DispWait(string msg)
    {
        await DisplayAlert("OK", msg, "OK");
    }

    //[Obsolete]
    private void RegisterEvent()
    {
        //ボタン
        BtnCustomerList.Clicked += Button_Clicked;
        BtnInOutMoney.Clicked += Button_Clicked;
        BtnBusSell.Clicked += Button_Clicked;
        BtnReport.Clicked += Button_Clicked;
        BtnCurrencyGood.Clicked += Button_Clicked;
        BtnEmployee.Clicked += Button_Clicked;
        VwFontTestGridBase.Clicked += Button_Clicked;

        //VwFontTestGridBase.Clicked += Button_Clicked;
        //VwFontTestLayoutBase.Clicked += Button_Clicked;

        //顧客テスト.Clicked += Button_Clicked;
        //バインドテスト開始.Clicked += Button_Clicked;
        //バインドテスト終了.Clicked += Button_Clicked;

        this.Loaded += Window_Loaded;



    }

    private void Window_Loaded(object sender, EventArgs e)
    {
        try
        {
            var parentWindow = this.GetParentWindow();

            Maui.DataGrid.PaletteCollection paletCollection = this.RowPalette;

            //var window = this.GetParentWindow().Handler?.PlatformView as MauiWinUIWindow;     //こちらでも動作する。
            var mauiWindow = this.Window.Handler?.PlatformView as MauiWinUIWindow;
            if (mauiWindow == null)
            {
                HLog.Err("vw1 Window_Loaded Window Null Error");
                return;
            }

            HWindow hWindow = GIc.GetWindow();
            hWindow.SetWindowPattern(mauiWindow, WindowStyle.NoAlwaysOnTopResize);
            hWindow.ChangeMaximize(hWindow.GetAppWindow(mauiWindow), true);
        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }

    }

    private void Button_Clicked(object? sender, EventArgs e)
    {
        try
        {
            string? textName=string.Empty;

            if (sender is Button button)
            {
                textName = button.Text;
            }
            else if (sender is MenuFlyoutItem menuItem)
            {
                textName = menuItem.Text;
            }

            StopWatch timeMeasureBtn = new ("処理時間計測デバッグ:Vw1:Button_Clicked:画面呼び出しトリガーの処理時間::クリックされたボタン::" + textName);
            timeMeasureBtn.Start();

            if (textName == "顧客")
            {
                _hWindow.GoToAsync(nameof(Vw21));
                //HWindow.CreateWindow(new Vw21(), HWindow.EWindowHierarchyLevel.SecondFunctionList);
            }
            else if ((textName == "入出金") || (textName == "入出金一覧(Vw3)"))
            {
                _hWindow.GoToAsync(nameof(Vw3));
                //HWindow.CreateWindow(new Vw3(), HWindow.EWindowHierarchyLevel.SecondFunctionList);
            }
            else if (textName == "売買決済")
            {
                //HWindow.CreateWindow(new Vw41(), HWindow.EWindowHierarchyLevel.LastFunctionDetail);
                _hWindow.CreateWindow(new Vw41(), 0.7, 0.7);
            }
            else if (textName == "帳票")
            {
                //HWindow.CreateWindow(new RegisterGood(), HWindow.EWindowHierarchyLevel.LastFunctionDetail);
            }
            else if (textName == "商品・為替")
            {
                _hWindow.GoToAsync("//Vw5");
                //Shell.Current.GoToAsync("//Vw5");
            }
            else if (textName == "所属・社員")
            {
                _hWindow.CreateWindow(new Vw61Employee(), 0.4,0.8);
            }
            else if (textName == "詳細画面テンプレート")
            {
                _hWindow.CreateWindow(new VwFontTestGridBase(), 0.7, 0.7);
            }
            else if (textName == "フォントレイアウトテスト")
            {
                _hWindow.CreateWindow(new VwFontTestLayoutBase(), 0.8,0.8);
            }
            else if (textName == "顧客テスト")
            {

                ECustomer customer = GIc.GetVmCmr().GetDetailCustomerTest();

                customer.EmpPhoneNumber.FirstNumber = "06";
                customer.EmpPhoneNumber.MiddleNumber = "6208";
                customer.EmpPhoneNumber.LastNumber = "2607";

                //////アクティブウィンドウを閉じる
                //App.Current?.CloseWindow(GetParentWindow());
            }
            timeMeasureBtn.StopAndShowTime();

        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }
    }


    ////private void RegisterEvent2(Layout layout)
    ////{
    ////    foreach (var child in layout.Children)
    ////    {
    ////        if (child is Button button)
    ////        {
    ////            HLog.Info("Vw1:Button:Text" + button.Text);

    ////            button.Clicked += Button_Clicked;
    ////        }
    ////        else if (child is Layout nestedLayout)
    ////        {
    ////            RegisterEvent2(nestedLayout); // 再帰的に呼び出す
    ////        }
    ////    }
    ////}


}