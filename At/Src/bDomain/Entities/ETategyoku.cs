namespace CO2.At.Src.bDomain.Entities;

public sealed class ETategyoku
{
    private uint transactionID;
    private string productName;
    private string buySellCategory;
    private uint numberOfSheets;
    private DateTime establishmentTime;
    private decimal establishmentPrice;
    private decimal standardPrice;
    private decimal exchangeRate;
    private decimal marktoMarketProfitAndLoss;   ////値洗い差損益金
    private decimal marktoMarketDeductionAmount;  ////値洗い差引き額

    public ETategyoku()
    {
        transactionID = 0;
        productName = string.Empty;
        buySellCategory = string.Empty;
        numberOfSheets = 0;
        establishmentTime = DateTime.MaxValue;
        establishmentPrice = 0;
        standardPrice = 0;
        exchangeRate = 0;
        marktoMarketProfitAndLoss = 0;
        marktoMarketDeductionAmount = 0;
    }

    public uint TransactionID
    {
        get { return transactionID; }
        set { transactionID = value; }
    }

    public string ProductName
    {
        get { return productName; }
        set { productName = value; }
    }

    public string BuySellCategory
    {
        get { return buySellCategory; }
        set { buySellCategory = value; }
    }

    public uint NumberOfSheets
    {
        get { return numberOfSheets; }
        set { numberOfSheets = value; }
    }

    public DateTime EstablishmentTime
    {
        get { return establishmentTime; }
        set { establishmentTime = value; }
    }

    public decimal EstablishmentPrice
    {
        get { return establishmentPrice; }
        set { establishmentPrice = value; }
    }

    public decimal StandardPrice
    {
        get { return standardPrice; }
        set { standardPrice = value; }
    }

    public decimal ExchangeRate
    {
        get { return exchangeRate; }
        set { exchangeRate = value; }
    }

    public decimal MarktoMarketProfitAndLoss
    {
        get { return marktoMarketProfitAndLoss; }
        set { marktoMarketProfitAndLoss = value; }
    }

    public decimal MarktoMarketDeductionAmount
    {
        get { return marktoMarketDeductionAmount; }
        set { marktoMarketDeductionAmount = value; }
    }

    public void Set(uint transaction_id, string product_name, string buy_sell_category, uint number_of_sheets, DateTime establishment_time, decimal establishment_price, decimal standar_price, decimal exchange_rate, decimal markto_market_profit_and_loss, decimal markto_market_deduction_amount)
    {
        transactionID = transaction_id;
        productName = product_name;
        buySellCategory = buy_sell_category;
        numberOfSheets = number_of_sheets;
        establishmentTime = establishment_time;
        establishmentPrice = establishment_price;
        standardPrice = standar_price;
        exchangeRate = exchange_rate;
        marktoMarketProfitAndLoss = markto_market_profit_and_loss;
        marktoMarketDeductionAmount = markto_market_deduction_amount;
    }
}
