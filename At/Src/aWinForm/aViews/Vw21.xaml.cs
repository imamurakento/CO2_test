using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Helpers;
using static CO2.At.Src.bDomain.Helpers.CursorIconHelper;       //Todo:static�̈Ӗ������₷��

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

            ////AkgulGrid��^����
            Color rowColorBase = HRreources.GetColor("RowPalette");
            RowPalette.Add(rowColorBase);

            //UI�ɂ���S�Ẵ{�^���v�f���擾���z�o�[���Ƀn���h�ɂ���
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
        // �{�^���̃N���b�N�C�x���g��Command��Window��n��
        CustomerCancel.CommandParameter = this.GetParentWindow();
        CustomerSelect.CommandParameter = this.GetParentWindow();

        //�O���b�h�̐擪�s��I������
        //SimpleFactory.Instance.SetFirstIndex();
        GIc.GetVmCmr().SetFirstIndex();
    }

    private void OnButtonLoaded(object sender, EventArgs e)
    {
        //UI�ɂ���S�Ẵ{�^���v�f���擾���z�o�[���Ƀn���h�ɂ���
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
    //            //// �{�^���̖��O�ƃe�L�X�g���擾
    //            string buttonText = button.Text;

    //            string catMsg = nameof(Vw21) + "�@�{�^������:" + buttonText + " :";

    //            if (buttonText == "�I��")
    //            {
    //                //ToDo:�����Vm���łǂ����s���邩
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