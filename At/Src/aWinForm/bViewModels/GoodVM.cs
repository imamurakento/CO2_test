using System.Collections.ObjectModel;
using CO2.At.Src.aFunc.GoodCurrency;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aWinForm.aViews;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UraniumUI.Material.Controls;

namespace CO2.At.Src.aWinForm.bViewModels;

internal partial class GoodVM : ObservableObject
{

    public enum ActionType
    {
        Currency = 0,
        Good = 1,
    }

    private readonly HWindow _hWindow;
    private readonly IGood _cyRepo;

    private int _crudMode;
    public ActionType ActionMode;


    [ObservableProperty]
    private ObservableCollection<ECurrency>? _currencysList;
    [ObservableProperty]
    private ObservableCollection<EGood>? _goodList;

    [ObservableProperty]
    private ECurrency _selectedCurrency;
    [ObservableProperty]
    private EGood _selectedGood;

    [ObservableProperty]
    private ECurrency? _detailCurrency;

    [ObservableProperty]
    private string _gridColumTitle1 = string.Empty;
    [ObservableProperty]
    private string _gridColumTitle2 = string.Empty;
    [ObservableProperty]
    private string _gridColumTitle3 = string.Empty;
    [ObservableProperty]
    private string _gridColumTitle4 = string.Empty;

    [ObservableProperty]
    private string _gridColumData1 = string.Empty;
    [ObservableProperty]
    private string _gridColumData2 = string.Empty;
    [ObservableProperty]
    private string _gridColumData3 = string.Empty;
    [ObservableProperty]
    private string _gridColumData4 = string.Empty;



    public GoodVM(IGood currencyRepo)
    {

        StopWatch hStop = new("処理時間計測デバッグ:GoodVM ");
        hStop.Start();

        _hWindow = GIc.GetWindow();

        _cyRepo = currencyRepo;
        _currencysList = new ObservableCollection<ECurrency>();
        _goodList = new ObservableCollection<EGood>();

        _selectedCurrency = new ECurrency();
        _detailCurrency = new ECurrency();

        ActionMode = ActionType.Currency;
        ChgFuncMode(ActionType.Currency.ToString());

        LoadC();
        LoadG();

        hStop.StopAndShowTime();

    }

    [RelayCommand]
    private void ShowCurrencyDetail()
    {
        if (ActionMode == ActionType.Currency)
        {
            _hWindow.CreateWindow(new Vw523_CurrencyDetail(), 0.4, 0.8);
        }
        else if (ActionMode == ActionType.Good)
        {
            _hWindow.CreateWindow(new Vw522_GoodDetai(), 0.4, 0.8);
        }
    }

    [RelayCommand]
    private void CloseWindow(Window window)
    {
        //Application.Current?.CloseWindow(window);
        _hWindow.GoToAsync(nameof(Vw1));
    }

    [RelayCommand]
    private void ChgFuncMode(string actionMode)
    {
        HLog.Info("GoodVM: ChgFuncMode");
        //int actMode = Convert.ToInt32(actionMode);
        //_actionMode = (ActionType)actMode;
        //if (_actionMode == ActionType.Currency)
        //{
        //    GridColumTitle1 = "基準通貨名";
        //    GridColumTitle2 = "基準レート";
        //    GridColumTitle3 = "単位";
        //    GridColumTitle4 = "更新日時";
        //}
        //else if (_actionMode == ActionType.Good)
        //{
        //    GridColumTitle1 = "商品名";
        //    GridColumTitle2 = "基準値段";
        //    GridColumTitle3 = "基準通貨";
        //    GridColumTitle4 = "取引単位";
        //}
        if (actionMode == "0")
        {
            GridColumTitle1 = "基準通貨名";
            GridColumTitle2 = "基準レート";
            GridColumTitle3 = "単位";
            GridColumTitle4 = "更新日時";
            GridColumData1 = "BaseRate";
        }
        else if (actionMode == "1")
        {
            GridColumTitle1 = "商品名";
            GridColumTitle2 = "基準値段";
            GridColumTitle3 = "基準通貨";
            GridColumTitle4 = "取引単位";
            GridColumData1 = "UpdatedAt";
        }

        DataGridColumn dc;
    }



    public void SetFirstIndex()
    {

        if (CurrencysList == null || CurrencysList.Any() == false)
        {
            return;
        }

        SelectedCurrency = CurrencysList[0];
    }

    private void UpdateListByUIThread(List<ECurrency> cList)
    {

        StopWatch hStop = new("処理時間計測デバッグ:UpdateListByUIThread関数");
        hStop.Start();

        CurrencysList.Clear();
        foreach (var currency in cList)
        {
            CurrencysList.Add(currency);
        }


        ////UIスレッドで更新
        //MainThread.BeginInvokeOnMainThread(() =>
        //{
        //    CurrencysList.Clear();
        //    foreach (var Currency in cList)
        //    {
        //        CurrencysList.Add(Currency);
        //    }
        //});

        hStop.StopAndShowTime();
    }


    private void LoadC()
    {

        StopWatch hStop = new("処理時間計測デバッグ:Currency LoadC関数");
        hStop.Start();

        //List<ECurrency>? getLists = _cyRepo.Load();
        UpdateListByUIThread(_cyRepo.LoadC());

        hStop.StopAndShowTime();

    }

    private void LoadG()
    {

        StopWatch hStop = new("処理時間計測デバッグ:Currency LoadG関数");
        hStop.Start();

        List<EGood>? getLists = _cyRepo.LoadG();
        ////UIスレッドで更新
        MainThread.BeginInvokeOnMainThread(() =>
        {
            GoodList.Clear();
            foreach (var good in getLists)
            {
                GoodList.Add(good);
            }
        });

        hStop.StopAndShowTime();

    }


}
