using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.Employee;
using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.aLogics;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw41 : ContentPage
{
    private readonly CmrVM? _vmCustomer;

    public Vw41()
    {
        InitializeComponent();
        _vmCustomer = GIc.GetVmCmr();

        BindingContext = _vmCustomer;
    }
}