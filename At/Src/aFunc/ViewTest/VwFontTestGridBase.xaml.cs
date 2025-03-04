using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.Employee;
using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.aLogics;

namespace CO2.At.Src.aFunc.ViewTest;

public partial class VwFontTestGridBase : ContentPage
{
    private readonly CmrVM? _vmCustomer;

    public VwFontTestGridBase()
    {
        InitializeComponent();
        _vmCustomer = GIc.GetVmCmr();

        BindingContext = _vmCustomer;
    }
}