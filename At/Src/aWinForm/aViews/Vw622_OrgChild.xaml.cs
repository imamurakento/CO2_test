using CO2.At.Src.aFunc.Employee;
using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.eValueObject;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw622_OrgChild : ContentPage
{

    private readonly EmpVM? _vmEmployee;
    private readonly CrudPrm _crudKind;

    public Vw622_OrgChild()
    {
        InitializeComponent();

        _vmEmployee = GIc.GetEmpVm();

        BindingContext = _vmEmployee;

        _crudKind = _vmEmployee.CrudTarget;

    }
}