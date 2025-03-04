using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Employee;

public partial class EEmp : ObservableObject
{

    [ObservableProperty]
    private string _empName = string.Empty;

    [ObservableProperty]
    private int _orgChildId = 0;

    public int EmpId { get; set; } = 0;

    public bool EffectiveEndDate { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.MaxValue;

    public DateTime UpdatedAt { get; set; } = DateTime.MaxValue;

    public int UpdatedUser { get; set; } = 0;

    public bool IsOldExistence { get; set; } = false;

    public void Set(int empId, string empName, int orgChildId)
    {
        EmpId = empId;
        EmpName = empName;
        OrgChildId = orgChildId;
    }

}
