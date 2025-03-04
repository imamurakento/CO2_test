using Microsoft.Maui.Controls;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw523_CurrencyDetail : ContentPage
{
    public Vw523_CurrencyDetail()
    {
        InitializeComponent();
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string currency = CurrencyEntry.Text;
        string unit = UnitEntry.Text;

        await DisplayAlert("“o˜^Š®—¹", $"’Ê‰Ý: {currency}, ’PˆÊ: {unit} ‚ª“o˜^‚³‚ê‚Ü‚µ‚½", "OK");

        Application.Current?.CloseWindow(GetParentWindow());
        return;
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        Application.Current?.CloseWindow(GetParentWindow());
        return;
    }
}