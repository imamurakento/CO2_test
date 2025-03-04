using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;


#pragma warning disable SA1201

namespace CO2.At.Src.aFunc.Employee;

public partial class EParent : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<EChild>? _orgChildList;

    //// フィールド (データバインディングあり)
    [ObservableProperty]
    private string _orgParentName = string.Empty;

    //// フィールド (データバインディングなし)
    public int OrgParentId { get; set; } = 0;

    //// プロパティ (データバインディングなし)
    public bool DeleteFlag { get; set; } = false;
    public bool EffectiveEndDate { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.MaxValue;
    public DateTime UpdatedAt { get; set; } = DateTime.MaxValue;
    public int UpdatedUser { get; set; } = 0;
    public bool IsOldExistence { get; set; } = false;

    public EParent()
    {
        _orgChildList = new ObservableCollection<EChild>();
    }

    public void Set(string orgParentName, int orgParentId = 0, bool deleteFlag = false)
    {
        OrgParentName = orgParentName;
        OrgParentId = orgParentId;
        DeleteFlag = deleteFlag;
    }
}

#pragma warning disable SA1201

