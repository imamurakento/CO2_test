using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Employee;

public partial class EChild : ObservableObject
{

    //// データバインディングを利用するフィールド
    [ObservableProperty]
    private string _orgChildName = string.Empty;

    [ObservableProperty]
    private int _orgParentId = 0;

    //// データバインディングを利用しないプロパティ
    public int OrgChildId { get; set; } = 0;
    public bool DeleteFlag { get; set; } = false;
    public bool EffectiveEndDate { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.MaxValue;
    public DateTime UpdatedAt { get; set; } = DateTime.MaxValue;
    public int UpdatedUser { get; set; } = 0;
    public bool IsOldExistence { get; set; } = false;
    public string Furigana { get; set; } = string.Empty;

    public void Set(int orgChildId, string orgChildName, int orgParentId)
    {
        OrgChildId = orgChildId;
        OrgChildName = orgChildName;
        OrgParentId = orgParentId;
    }


}
