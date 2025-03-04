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

    //�o�^�{�^���������ꂽ���̏���
    private void RegisterButtonClicked(object sender, EventArgs e)
    {
        // �G���g���[����l���擾������
        double num1 = double.Parse(productNameEntry.Text);
        double num2 = double.Parse(tradeUnitEntry.Text);
        sum = num1 + num2;


        //�a���G���g���[�ɕ\��
        depositEntry.Text = sum.ToString();

        //�a��DisplayAlert�ɕ\��
        DisplayAlert("�m�F", sum.ToString(), "OK");

        // NewContent1 (TestPage3) �̃E�B���h�E���J��
    }

    // �L�����Z���{�^���������ꂽ���̏���
    private void CancelButton_Clicked(object sender, EventArgs e)
    { //// �e�̃E�B���h�E���擾���ĕ���
        //var currentWindow = Application.Current.Windows.FirstOrDefault(w => w.Handler?.PlatformView is Microsoft.Maui.Controls.Handlers.WindowHandler);
        //if (currentWindow != null)
        //{
        //    Application.Current.CloseWindow(currentWindow);
        //}


        //���݂̃E�B���h�E���擾���ĕ���
        Application.Current?.CloseWindow(GetParentWindow());
    }
}
