using CommunityToolkit.Mvvm.ComponentModel;

#pragma warning disable SA1201

namespace CO2.At.Src.aFunc.GoodCurrency;

public partial class ECurrency : ObservableObject
{
    [ObservableProperty]
    private decimal _baseRate = 0m;                     //通貨_基準レートテーブル側

    [ObservableProperty]
    private DateTime _updatedAt = DateTime.MaxValue;    //通貨_基準レートテーブル側

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _unit = string.Empty;

    public int BaseCurrencyId { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.MaxValue;
    public DateTime EffectiveStartDate { get; set; } = DateTime.MaxValue;
    public bool DeleteFlag { get; set; } = false;
    public DateTime EffectiveEndDate { get; set; } = DateTime.MaxValue;
    public bool IsOldExistence { get; set; } = false;

    public void Set(
        decimal baseRate,
        DateTime updatedAt,
        int baseCurrencyId,
        string name,
        string unit)
    {
        BaseRate = baseRate;
        UpdatedAt = updatedAt;
        BaseCurrencyId = baseCurrencyId;
        Name = name;
        Unit = unit;
    }


}

#pragma warning disable SA1201

