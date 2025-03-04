using System.Collections.ObjectModel;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.ViewTest;
using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.Helpers;

////using Microsoft.Office.Interop.Excel;
using static CO2.At.Src.bDomain.Helpers.CursorIconHelper;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace CO2.At.Src.aWinForm.aViews;


public partial class Vw5 : ContentPage
{
    private GoodVM _cyVM;

    public Vw5()
    {

        try
        {

            StopWatch hStop = new("処理時間計測デバッグ:Vw5 コンストラクタ ");
            hStop.Start();

            InitializeComponent();

            HLog.Info("Vw5:InitializeComponent: OK");

            ////ToDo:コンストラクタではできる。
            //Vw5Grid.Columns[0].PropertyName = "UpdatedAt";

            ////AkgulGrid定型処理
            Color rowColorBase = HRreources.GetColor("ColorBase");
            RowPalette.Add(rowColorBase);
            RowPaletteGood.Add(rowColorBase);

            _cyVM = GIc.GetVmCy();
            BindingContext = _cyVM;

            //UIにある全てのボタン要素を取得しホバー時にハンドにする
            Dispatcher.Dispatch(() =>
            {
                if (this.Handler?.MauiContext is IMauiContext mauiContext)
                {
                    ApplyCursorToAllButtons(this.Content as VisualElement, CursorIcon.Hand, mauiContext);
                    ApplyAnimationToAllButtons(this.Content as VisualElement);
                }
            });

            hStop.StopAndShowTime();

        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //ToDo:切替はきかない
        //Vw5Grid.Columns[0].PropertyName = "UpdatedAt";

        _cyVM.ActionMode = GoodVM.ActionType.Good;
        Vw5Grid.IsVisible = false;
        Vw5Grid.IsEnabled = false;
        Vw5Grid.ZIndex = 0;
        Vw5GridGood.IsVisible = true;
        Vw5GridGood.IsEnabled = true;
        Vw5GridGood.ZIndex = 10;


    }

    //通貨切替テスト
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        _cyVM.ActionMode = GoodVM.ActionType.Currency;
        Vw5Grid.IsVisible = true;
        Vw5Grid.IsEnabled = true;
        Vw5Grid.ZIndex = 10;
        Vw5GridGood.IsVisible = false;
        Vw5GridGood.IsEnabled = false;
        Vw5GridGood.ZIndex = 0;

    }

    //private void OnButtonLoaded(object sender, EventArgs e)
    //{
    //    //UIにある全てのボタン要素を取得しホバー時にハンドにする
    //    Dispatcher.Dispatch(() =>
    //    {
    //        if (this.Handler?.MauiContext is IMauiContext mauiContext)
    //        {
    //            ApplyCursorToAllButtons(this.Content as VisualElement, CursorIcon.Hand, mauiContext);
    //            ApplyAnimationToAllButtons(this.Content as VisualElement);
    //        }
    //    });
    //}

}