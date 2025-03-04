using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.Employee;
using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.aLogics;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw621_OrgParent : ContentPage
{

    private readonly EmpVM _vmEmployee;

    public Vw621_OrgParent()
    {
        InitializeComponent();
        _vmEmployee = GIc.GetEmpVm();
    }
}