using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Helpers;
using static CO2.At.Src.bDomain.Helpers.CursorIconHelper;       //Todo:staticの意味を質問する

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw21 : ContentPage
{
    private readonly bViewModels.CmrVM? _vmCustomer;
    private readonly VmToViewMsg? _vmToRecvMsg;

    public Vw21()
    {
        try
        {
            InitializeComponent();

            HLog.Info("Vw21:InitializeComponent: OK");

            _vmCustomer = GIc.GetVmCmr();
            BindingContext = _vmCustomer;

            _vmToRecvMsg = _vmCustomer.ToVw21Msg;
            _vmToRecvMsg.SetTargetPage(this);

            ////AkgulGrid定型処理
            Color rowColorBase = HRreources.GetColor("RowPalette");
            RowPalette.Add(rowColorBase);

            //UIにある全てのボタン要素を取得しホバー時にハンドにする
            Dispatcher.Dispatch(() =>
            {
                if (this.Handler?.MauiContext is IMauiContext mauiContext)
                {
                    ApplyCursorToAllButtons(this.Content as VisualElement, CursorIcon.Hand, mauiContext);
                    ApplyAnimationToAllButtons(this.Content as VisualElement);
                }
            });
        }
        catch (Exception ex)
        {
            HLog.Err("Vw21:Constructor:" + ex.Message);
        }
    }

    private void Window_Loaded(object sender, EventArgs e)
    {
        // ボタンのクリックイベントでCommandにWindowを渡す
        CustomerCancel.CommandParameter = this.GetParentWindow();
        CustomerSelect.CommandParameter = this.GetParentWindow();

        //グリッドの先頭行を選択する
        //SimpleFactory.Instance.SetFirstIndex();
        GIc.GetVmCmr().SetFirstIndex();
    }

    private void OnButtonLoaded(object sender, EventArgs e)
    {
        //UIにある全てのボタン要素を取得しホバー時にハンドにする
        Dispatcher.Dispatch(() =>
        {
            if (this.Handler?.MauiContext is IMauiContext mauiContext)
            {
                ApplyCursorToAllButtons(this.Content as VisualElement, CursorIcon.Hand, mauiContext);
                ApplyAnimationToAllButtons(this.Content as VisualElement);
            }
        });
    }

    //private void Button_Clicked(object? sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (sender is Button button)
    //        {
    //            //// ボタンの名前とテキストを取得
    //            string buttonText = button.Text;

    //            string catMsg = nameof(Vw21) + "　ボタン処理:" + buttonText + " :";

    //            if (buttonText == "選択")
    //            {
    //                //ToDo:これをVm側でどう実行するか
    //                Shell.Current.GoToAsync("////Vw1");
    //                Application.Current?.CloseWindow(GetParentWindow());
    //                return;
    //            }
    //        }
    //        ////Shell.Current.GoToAsync(nameof(Vw22));
    //    }
    //    catch (Exception ex)
    //    {
    //        HLog.Err("Vw21:Button_Clicked:" + ex.Message);
    //    }
    //}




}