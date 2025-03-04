using CommunityToolkit.Mvvm.ComponentModel;

#pragma warning disable SA1201

namespace CO2.At.Src.aFunc.GoodCurrency;

public partial class EGood : ObservableObject
{
    [ObservableProperty]
    private decimal _standardPrice = 0;                 //商品_基準レートテーブル側

    [ObservableProperty]
    private DateTime _updatedAt = DateTime.MaxValue;    //商品_基準レートテーブル側

    [ObservableProperty]
    private string _productName = string.Empty;

    [ObservableProperty]
    private int _baseCurrencyId = 0;

    [ObservableProperty]
    private string _transactionUnit = string.Empty;

    [ObservableProperty]
    private decimal _transactionMargin = 0;

    [ObservableProperty]
    private decimal _basicFee = 0;

    [ObservableProperty]
    private int _minimumQuantity = 0;

    public int ProductId { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.MaxValue;
    public DateTime EffectiveStartDate { get; set; } = DateTime.MaxValue;
    //public DateTime UpdatedAt { get; set; } = DateTime.MaxValue;
    public bool DeleteFlag { get; set; } = false;
    public DateTime EffectiveEndDate { get; set; } = DateTime.MaxValue;
    public bool IsOldExistence { get; set; } = false;

    public void Set(
        decimal standardPrice,
        DateTime updatedAt,
        int productId,
        string productName,
        int baseCurrencyId,
        string transactionUnit,
        decimal transactionMargin,
        decimal basicFee,
        int minimumQuantity)
    {
        StandardPrice = standardPrice;
        UpdatedAt = updatedAt;
        ProductId = productId;
        ProductName = productName;
        BaseCurrencyId = baseCurrencyId;
        TransactionUnit = transactionUnit;
        TransactionMargin = transactionMargin;
        BasicFee = basicFee;
        MinimumQuantity = minimumQuantity;
    }

}

#pragma warning disable SA1201

