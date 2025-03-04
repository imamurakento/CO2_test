using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.Helpers;

namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw331_Ledger : ContentPage
{
    private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

    public Vw331_Ledger()
    {
        InitializeComponent();
        this.BindingContext = new TransactionsViewModel();

        ////AkgulGrid定型処理
        Color rowColorBase = HRreources.GetColor("ColorBase");
        RowPalette.Add(rowColorBase);

    }

    ////    private void OnCustomerSetting(object sender, EventArgs e)
    ////    {
    ////        Logger.Info("OnCustomerSetting Called!");
    ////
    ////        ////マルチウィンドウサンプル
    ////        var testWindow = new Window(new CustomerSetting())
    ////        {
    ////            //// Set fixed width and height
    ////            Width = 1000,  //// Set your desired width
    ////            Height = 700,  //// Set your desired height
    ////            MaximumWidth = 1000,
    ////            MaximumHeight = 700,
    ////            MinimumWidth = 1000,
    ////            MinimumHeight = 700
    ////            //// Prevent resizing
    ////        };
    ////        Application.Current.OpenWindow(testWindow);
    ////    }

    public class Transaction
    {
        public int TransactionID { get; set; }

        public string? GoodName { get; set; }

        public DateTime SellingTime { get; set; }

        public decimal SellingPrice { get; set; }

        public int SellingNumber { get; set; }

        public decimal SellingSwap { get; set; }

        public DateTime BuyingTime { get; set; }

        public decimal BuyingPrice { get; set; }

        public int BuyingNumber { get; set; }

        public decimal BuyingSwap { get; set; }

        public decimal MarginAndOtherExchangeRate { get; set; }

        public decimal MarginAndOtherGainOnSales { get; set; }

        public decimal MarginAndOtherCommission { get; set; }

        public decimal MarginAndOtherConsumptionTax { get; set; }

        public decimal MarginAndOtherProfitAndLoss { get; set; }
    }

    public class TransactionsViewModel
    {
        public TransactionsViewModel()
        {
            Transactions = new ObservableCollection<Transaction>
            {
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                new ()
                {
                    TransactionID = 1,
                    GoodName = "Widget",
                    SellingTime = DateTime.Now,
                    SellingPrice = 20.00m,
                    SellingNumber = 100,
                    SellingSwap = 0.50m,
                    BuyingTime = DateTime.Now.AddDays(-1),
                    BuyingPrice = 15.00m,
                    BuyingNumber = 80,
                    BuyingSwap = 0.30m,
                    MarginAndOtherExchangeRate = 1.1m,
                    MarginAndOtherGainOnSales = 5.00m,
                    MarginAndOtherCommission = 1.00m,
                    MarginAndOtherConsumptionTax = 0.75m,
                    MarginAndOtherProfitAndLoss = 3.45m,
                },
                //// Add more sample transactions as needed
            };
        }

        public ObservableCollection<Transaction> Transactions { get; set; }
    }
}