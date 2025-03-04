namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw32 : ContentPage
{
    public Vw32()
    {
        InitializeComponent();
        this.BindingContext = new EnExAmountViewModel();

        //SFGrid.InitBaseStyle(SFGrid_Vw3);
    }

    private void CloseBtn_Clicked(object sender, EventArgs e)
    {
        Application.Current?.CloseWindow(GetParentWindow());
    }

    private void DateSelected(object sender, EventArgs e)
    {
        ShowDate.Text = DatePicker.Date.ToString("yyyy”NMMŒŽdd“ú");
    }
}