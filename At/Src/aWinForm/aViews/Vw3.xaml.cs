using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Helpers;
using WinRT;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw3 : ContentPage
{
    private HWindow _hWindow;

    public Vw3()
    {
        InitializeComponent();
        this.BindingContext = new EnExAmountViewModel();

        ////AkgulGrid定型処理
        Color rowColorBase = HRreources.GetColor("ColorBase");
        RowPalette.Add(rowColorBase);

        _hWindow = GIc.GetWindow();

    }

    private void BuySell_Clicked(object sender, EventArgs e)
    {
        _hWindow.CreateWindow(new Vw32(), 0.4, 0.6);
    }

    private void TransHis_Clicked(object sender, EventArgs e)
    {
        _hWindow.CreateWindow(new Vw331_Ledger(), 0.4,0.6);
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        _hWindow.GoToAsync(nameof(Vw1));
    }

}

public class EnExAmount
{
    public int No { get; set; }

    public string? Time { get; set; }

    public string? EnExClassification { get; set; }

    public decimal AmountOfMoney { get; set; }

    public string? DepCertNumber { get; set; }
}

public class EnExAmountViewModel
{
    public EnExAmountViewModel()
    {
        EnExAmounts = new ObservableCollection<EnExAmount>();
        decimal baseMoney = 1000;
        int depCertNumberBase = 788000;

        for (int i = 0; i < 500; i++)
        {
            EnExAmount enExAmount = new();

            enExAmount.No = i;
            enExAmount.Time = "2025/02/11 10:00";

            if ((i % 4) == 0)
            {
                enExAmount.EnExClassification = "入金";
            }
            else if ((i % 4) == 1)
            {
                enExAmount.EnExClassification = "出金";
            }
            if ((i % 4) == 2)
            {
                enExAmount.EnExClassification = "振替入金";
            }
            else if ((i % 4) == 3)
            {
                enExAmount.EnExClassification = "振替出金";
            }

            enExAmount.AmountOfMoney = baseMoney + i;
            depCertNumberBase = depCertNumberBase + i;
            enExAmount.DepCertNumber = depCertNumberBase.ToString();
            EnExAmounts.Add(enExAmount);
        }
    }



    public ObservableCollection<EnExAmount> EnExAmounts { get; set; }
}