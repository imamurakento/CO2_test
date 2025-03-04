using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Helpers;
//using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Maui.Controls.Application;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw22 : ContentPage
{
    private readonly CmrVM? _vmCustomer;
    private readonly VmToViewMsg? _vmToRecvMsg;

    public Vw22()
    {
        try
        {
            InitializeComponent();

            HLog.Info("Vw22:InitializeComponent: OK");

            _vmCustomer = GIc.GetVmCmr();
            _vmToRecvMsg = _vmCustomer.ToVw22Msg;
            _vmToRecvMsg.SetTargetPage(this);

            BindingContext = _vmCustomer;

        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }

    }

    private void BtnCancel_Clicked(object? sender, EventArgs e)
    {
        Application.Current?.CloseWindow(GetParentWindow());
    }

    private void Window_Loaded(object sender, EventArgs e)
    {
        // �{�^���̃N���b�N�C�x���g��Command��Window��n��
        BtnCancel.CommandParameter = this.GetParentWindow();
    }

    //private void OnNumericEntryTextChanged(object sender, TextChangedEventArgs e)
    //    {
    //    var entry = sender as Entry;

    //    // ���͒l������
    //    if (!string.IsNullOrEmpty(entry.Text) && !int.TryParse(entry.Text, out _))
    //        {
    //        // �񐔒l�����͂��ꂽ�ꍇ�͒��O�̒l�ɖ߂�
    //        entry.Text = e.OldTextValue;
    //        PostalCodeErrorTxt.Text = "���͂ł���̂͐��l�݂̂ł��B";
    //        }
    //    }
    //private void OnEntryUnfocused(object sender, FocusEventArgs e)
    //{
    //    HLog.Info("FuriganaTxt Entry lost focus due to tab key press or another action.");
    //}

    //private async Task OnDisplayAlertRequested(string message)
    //{
    //    await DisplayAlert("�G���[", message, "OK");
    //}




}