using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Customer.Parts;

public partial class EmployeeInCharge : InputValidatorBase
{
    [ObservableProperty]
    private ObservableCollection<string>? _empInChargeList;

    [ObservableProperty]
    private string _selectedEmp;

    public EmployeeInCharge()
    {

        _empInChargeList = [
            "管理(特別)",
            "管理(通常)",
            "第一営業(通常)",
            "第二営業(通常)",
            "その他②",
            "その他①森本"];
        _selectedEmp = "管理(特別)";
    }
}
