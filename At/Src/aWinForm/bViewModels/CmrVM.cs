using System.Collections.ObjectModel;
using System.Text;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aWinForm.aViews;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.eObservableObject;
using CO2.At.Src.bDomain.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json.Linq;

namespace CO2.At.Src.aWinForm.bViewModels;

////重要項目：
////
////アーキテクチャ
////結論:DDD(ドメイン駆動設計)の集約とサービスはMVVMのViewModelに統合する。
////前提:CO2ではMVVM＋DDDを採用している。MVVMはUIの処理をViewModelに集約し、DDDはビジネスロジックをドメイン層に集約する。
////理由:CO2が要求されている機能のレベルがそこまで大きくないため、アーキテクチャ的に重複する部分が存在する。境界を分けるより、統合するメリットのほうが大きい。
////
////DBの同期操作の扱い
////結論:DBの操作は非同期ではおこなわず、基本同期で扱う。
////前提:DB系はタイムアウトの可能性また、Loadは何件あるか不明なため、本来は非同期で実行するほうが良い。
////理由:①DBの旧データがMicrosoft　Accessのデータを引き継いでおり、データの規模が大きくない。
////     ②CO2ではDBのデータはUIと密接に連携しているため、非同期処理の必要性も低い。
////     ③CO2ではアプリとDBが同一のPC上で動作するため、サーバーとの通信が発生しない。タイムアウトのリスクもほぼなく。この観点でも非同期処理の必要性は低い。
////MVVMアーキテクチャでは非同期DB処理の実装場所は、Model層またはService層が担当するべきである。
////シンプルなDBアクセス（CRUD）ならTask.Run()メソッドが一番シンプルであるが、あるべき姿はtoolkitのAsyncRelayCommandを利用するべき。
////結論はあまり必要としていない非同期処理をおこなうと、開発コストもかかるため、非同期は今後の検討課題とする。

////データバインディングとUIスレッドの関係
////データバィンディングしているデータを操作するためには、UIスレッドで行う必要がある。
////具体的にはObservableCollection系はUIスレッドで更新しないと例外が発生する。
////CO2ではリストの操作が具体的な場所となる。
////リストの操作はMainThread.BeginInvokeOnMainThread()を利用してUIスレッドで更新することを明確にする。
////
////リストのデータ構造
////結論:将来対応のためのリストの構造は分ける。リストの実体はViewModelが保持する。その他のクラスはリストを保持しない。
////理由:DB操作を非同期でおこなう場合、直接ObservableCollectionにアクセスできないため、ドメイン層とインフラストラクチャ層はObservableCollectionで保持する必要がない。
////リストのデータ型
////ViewModel側がObservableCollection<ECustomer>で管理する。リポジトリはList<ECustomer>型でデータ操作をおこなう。
////データリストをList<ECustomer>で受け取ったViewModelはObservableCollection<ECustomer>でリストの詰め替えをおこなう。
////ECustomerエンティティは実体をそのまま共有するので、ロジックの分離が可能となる。リポジトリは「データ取得」の責務のみを持たせる。

//Tmp:Git競合テスト用。後で消す。


internal partial class CmrVM : ObservableObject
{

    public enum CmrActionState
    {
        Normal,
        InAction,
    }
    public enum CmrViewMode
    {
        NowCustomer,
        PreviouslyCustomer,
    }

    public enum CmrDetailState
    {
        Create,
        Update,
        Read,
        Delete,
        Search,
        Select,
        Cancel,
    }

    private readonly HWindow _hWindow;
    private readonly ICustomer _customerRepo;

    private CmrDetailState _crudMode;
    private CmrActionState _actionState;

    [ObservableProperty]
    private ObservableCollection<ECustomer>? _customersList;
    [ObservableProperty]
    private ECustomer _selectedCustomer = new ();
    ////DetailCustomerは詳細画面で使用する。CRUD操作により切り替える。「新規」のときのみNewをおこない、「編集」と「詳細」は_selectedCustomerを参照する。
    [ObservableProperty]
    private ECustomer? _detailCustomer;
    [ObservableProperty]
    private bool _isEnabled;

    [ObservableProperty]
    private string _searchPersonalPhoneNumber=string.Empty;
    [ObservableProperty]
    private string _searchCompanyPhoneNumber = string.Empty;
    [ObservableProperty]
    private string _searchFurigana = string.Empty;
    [ObservableProperty]
    private string _searchFullName = string.Empty;

    public VmToViewMsg ToVw21Msg;
    public VmToViewMsg ToVw22Msg;

    public CmrVM(ICustomer customerRepo)
    {
        _customerRepo = customerRepo;

        _actionState = CmrActionState.Normal;
        _hWindow = GIc.GetWindow();

        _customersList = new ObservableCollection<ECustomer>();

        ToVw21Msg = new VmToViewMsg();
        ToVw22Msg = new VmToViewMsg();
        Load();


    }

    private bool CanTransit(CmrDetailState cmrDetailState)
    {
        if (_actionState == CmrActionState.Normal)
        {
            _crudMode = cmrDetailState;
            SetActionState(CmrActionState.InAction);
            return true;
        }

        if (_actionState == CmrActionState.InAction)
        {
            return false;
        }
        return false;
    }
    private void SetActionState(CmrActionState actionState)
    {
        _actionState = actionState;
    }


    public ECustomer GetDetailCustomerTest()
    {
        return DetailCustomer;
    }

    public void SetFirstIndex()
    {

        if (CustomersList == null || CustomersList.Any() == false)
        {
            return;
        }

        SelectedCustomer = CustomersList[0];
    }

    private void UpdateListByUIThread(List<ECustomer> cList)
    {

        StopWatch hStop = new("処理時間計測デバッグ:UpdateListByUIThread関数");
        hStop.Start();

        if (MainThread.IsMainThread)
        {
            CustomersList?.Clear();
            foreach (var customer in cList)
            {
                CustomersList?.Add(customer);
            }
        }
        else
        {
            HLog.Warn("非UIスレッドからUIスレッドへ切替");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CustomersList?.Clear();
                foreach (var customer in cList)
                {
                    CustomersList?.Add(customer);
                }
            });
        }

        hStop.StopAndShowTime();
    }


    private void Load()
    {

        HLog.Info("VmCustomer: Command :Load ");

        StopWatch hStop = new("処理時間計測デバッグ:Customer Load関数");
        hStop.Start();

        //List<ECustomer>? getLists = _customerRepo.Load();
        UpdateListByUIThread(_customerRepo.Load());

        hStop.StopAndShowTime();

        ////UIスレッドで更新
        //MainThread.BeginInvokeOnMainThread(() =>
        //{
        //    CustomersList.Clear();
        //    foreach (var customer in getLists)
        //    {
        //        CustomersList.Add(customer);
        //    }
        //});

        //UpdateListByUIThread(getLists);


    }

    [RelayCommand]
    private void LoadDeleteCustomers()
    {
        UpdateListByUIThread(_customerRepo.LoadDeleteCustomers());
    }

    [RelayCommand]
    //private void SearchAll() => Load();

    private void SearchAll()
    {
        Load();
    }

    private bool ChkCustomerSelect()
    {
        if (SelectedCustomer == null)
        {
            string msg = "顧客が正しく選択されていません。再度、正しく選択してください。";
            HLog.Warn(msg);
            Task task = ToVw21Msg.DispConfirmMsg(msg);
            return false;
        }
        return true;
    }


    [RelayCommand]
    private void ShowNewCustomer()
    {
        IsEnabled = true;
        HLog.Info("VmCustomer: Command :ShowNewCustomer ");
        _crudMode = CmrDetailState.Create;
        DetailCustomer = new ECustomer();
        DetailCustomer.CustomerNumber = _customerRepo.GetNewID();

        _hWindow.CreateWindow(new Vw22(), 0.4, 0.8);
    }

    [RelayCommand]
    private void ShowDetailCustomer(int customerNumber)
    {
        HLog.Info("VmCustomer: Command :ShowDetailCustomer ");
        _crudMode = CmrDetailState.Read;

        DetailCustomer = _customerRepo.Read(SelectedCustomer.CustomerNumber);
        _hWindow.CreateWindow(new Vw22(), 0.4, 0.8);

        //HWindow.CreateWindow(new Vw22(), HWindow.EWindowHierarchyLevel.TopMain);
        //HWindow.GoToAsync("////Vw22");
        IsEnabled = false;
    }

    [RelayCommand]
    private void ShowEditCustomer(int customerNumber)
    {
        HLog.Info("VmCustomer: Command :ShowEditCustomer ");
        IsEnabled = true;
        _crudMode = CmrDetailState.Update;
        DetailCustomer = _customerRepo.Read(SelectedCustomer.CustomerNumber);
        _hWindow.CreateWindow(new Vw22(), 0.4, 0.8);
    }

    [RelayCommand]
    private async Task RegisterCustomerAsync()
    {
        bool isConfirmed;
        HLog.Info("VmCustomer: Command :RegisterCustomer ");

        if (DetailCustomer == null || CustomersList == null)
        {
            HLog.Err("顧客オブジェクトがnullまたは顧客リストがnullです。");
            return;
        }

        string customerName = DetailCustomer.FullName;
        string actionName = string.Empty;
        if (_crudMode == CmrDetailState.Update)
        {
            actionName = "更新";
        }
        else if (_crudMode == CmrDetailState.Create)
        {
            actionName = "登録";
        }

        isConfirmed = await ToVw22Msg.DispQuestionMsg($"{customerName}さんを{actionName}してよろしいですか？");

        if (!isConfirmed)
        {
            return;     // ユーザーが「いいえ」を選択
        }

        if (_crudMode == CmrDetailState.Update)
        {
            _customerRepo.Update(DetailCustomer);
        }
        else if (_crudMode == CmrDetailState.Create)
        {
            if (_customerRepo.Create(DetailCustomer))
            {
                CustomersList.Add(DetailCustomer);
            }
        }

        await ToVw22Msg.DispConfirmMsg($"{customerName}さんのデータを{actionName}しました。").ConfigureAwait(false);

    }

    [RelayCommand]
    private async Task DeleteCustomer(int customerNumber)
    {
        int cNumber;
        if (SelectedCustomer != null)
        {
            cNumber = SelectedCustomer.CustomerNumber;
            if (cNumber != customerNumber)
            {
                HLog.Warn($"選択顧客と引数の顧客番号が一致していません。引数: {customerNumber} 選択顧客番号: {cNumber}");
            }
        }
        else
        {
            HLog.Warn($"リストが正しく選択されていません。再度正しく選択してから、再試行してください");
        }

        ECustomer? eCustomer = Find(customerNumber);
        if (CustomersList == null || eCustomer == null)
        {
            HLog.Err("顧客オブジェクトがnullまたは顧客リストがnullです。");
        }

        string customerName = eCustomer.FullName;

        bool isConfirmed = await ToVw21Msg.DispQuestionMsg($"{customerName}さんを本当に削除しますか？");

        if (!isConfirmed)
        {
            return;     // ユーザーが「いいえ」を選択
        }

        //bool isConfirmed = await ToVw21Msg.DispQuestionMsg("顧客データを削除しますか?");
        HLog.Info("VmCustomer: Command :DeleteCustomer ");

        if (_customerRepo.Delete(customerNumber))
        {
            CustomersList.Remove(eCustomer);
            SetFirstIndex();
        }
        StopWatch hStop = new("DeleteCustomer:Msg関数");
        hStop.Start();

        //OnErrorOccurred("顧客データを削除しました。今後は過去の顧客リストからのみ参照できます。").ConfigureAwait(false);
        await ToVw21Msg.DispConfirmMsg($"{customerName}さんのデータを削除しました。今後は過去の顧客からのみ参照できます。");

        hStop.StopAndShowTime();

    }

    [RelayCommand]
    private async Task SelectCustomerAsync(Window window)
    {

        if (ChkCustomerSelect())
        {
            return;
        }

        SelectedCustomer nowCustomer = GIc.GetVw1VM().GetSelectedCustomer();

        if (nowCustomer.FullName != SelectedCustomer.FullName)
        {
            bool isConfirmed = await ToVw21Msg.DispQuestionMsg($"{nowCustomer.FullName}さんから{SelectedCustomer.FullName}さんの運用状況へ切り替えますか?");
            if (!isConfirmed)
            {
                return;     // ユーザーが「いいえ」を選択
            }
        }

        SelectedCustomer newCustomer = new SelectedCustomer(
                                                            SelectedCustomer.CustomerNumber.ToString(),
                                                            SelectedCustomer.FullName,
                                                            SelectedCustomer.EmpPhoneNumber.GetPhoneNumber(),
                                                            SelectedCustomer.PsnPhoneNumber.GetPhoneNumber(),
                                                            SelectedCustomer.PsnMobileNumber.GetPhoneNumber());

        GIc.GetVw1VM().SetSelectedCustomer(newCustomer);

        //ToDo:下記の２つはうまく動作しない。
        //await HWindow.toastPush("BBBBB");
        //await Shell.Current.DisplayAlert("確認", "AAAAAA", "OK");

        //await ToVw21Msg.DispConfirmMsg($"{newCustomer.FullName}さんの運用状況へ切り替えました").ConfigureAwait(false);
        await ToVw21Msg.DispConfirmMsg($"{newCustomer.FullName}さんの運用状況へ切り替えました");

        await Task.Delay(100);          // ダイアログを閉じた後に少し待つ

        // ToDo:ダイアログを表示させると、Application.Current?.CloseWindow(window);でも、Shell.Current.GoToAsync("//Vw1");でも遷移できない。
        //Application.Current?.CloseWindow(window);
        await Shell.Current.GoToAsync("//Vw1");

    }

    private ECustomer? Find(int target_number)
    {
        return CustomersList?.FirstOrDefault(eCustomer => eCustomer.CustomerNumber == target_number);
    }


    [RelayCommand]
    private void SearchCustomer(string refineStr)
    {
        HLog.Info("VmCustomer: Command :SearchCustomer ");

        List<ECustomer>? getLists = _customerRepo.SearchByVowel(refineStr);
        ////UIスレッドで更新
        MainThread.BeginInvokeOnMainThread(() =>
        {
            CustomersList.Clear();
            foreach (var customer in getLists)
            {
                CustomersList.Add(customer);
            }
        });

    }

    [RelayCommand]
    private void SearchPartialCustomer(string testParam)
    {
        HLog.Info("VmCustomer: Command :SearchPartialCustomer ");

        string fullName = SearchFullName;
        string furigana = SearchFurigana;
        string personalPhone = SearchPersonalPhoneNumber;
        string companyPhone = SearchCompanyPhoneNumber;

        if (string.IsNullOrWhiteSpace(personalPhone) && string.IsNullOrWhiteSpace(companyPhone) && string.IsNullOrWhiteSpace(fullName) && string.IsNullOrWhiteSpace(furigana))
        {
            HLog.Warn("検索条件が空です。");
            return;
        }
        //ToDo:検索条件の組み合わせを考慮する
        //_customerRepo.PartialSearch("furigana", furigana);

        UpdateListByUIThread(_customerRepo.PartialSearch(fullName, furigana, personalPhone, companyPhone));

    }


    [RelayCommand]
    private void CloseWindow(Window window)
    {
        //Application.Current?.CloseWindow(window);
        //Shell.Current.GoToAsync("..");
        //if (CanTransit(CmrDetailState.Cancel))
        //{
        //    Shell.Current.GoToAsync("//Vw1");
        //}
        Shell.Current.GoToAsync("//Vw1");
    }


    [RelayCommand]
    private void ReportCustomer(int customerNumber)
    {
        HLog.Info("VmCustomer: Command :ReportCustomer ");

        //// 実行ファイルのディレクトリを基準に絶対パスを取得
        //string relativeIniFilePath = @"..\..\..\..\..\At\Dat\Setting\CO2.ini";
        //string basePath = AppContext.BaseDirectory;
        //string absoluteIniFilePath = Path.GetFullPath(Path.Combine(basePath, relativeIniFilePath));

        //ExcelReport lExcelReport = new (absoluteIniFilePath);

        //if (lExcelReport.Reflect_IniFile())
        //{
        //    if (SelectedCustomer == null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        ToCsv(lExcelReport.CsvPath, true, DetailCustomer);
        //        if (lExcelReport.RunExcelMacro())
        //        {
        //            lExcelReport.RunExcelPrint();
        //        }
        //        else
        //        {
        //            lExcelReport.ErrMsg = "CSVファイルへの書き込みに失敗しました。";
        //        }
        //    }

        //    if (lExcelReport.ErrMsg != string.Empty)
        //    {
        //    //DisplayErrorMessage(lExcelReport.ErrMsg, false);
        //    lExcelReport.ResetErr();
        //    }
        //else
        //    {
        //    //DisplayErrorMessage("印刷に成功しました", true);
        //    }
        //}
    }

    private bool ToCsv(string csv_path, bool is_Header, ECustomer eCustomer)
    {
        try
        {
            using (StreamWriter sw = new (csv_path, false, Encoding.GetEncoding("Shift-JIS")))
            {
                if (is_Header)
                {
                    sw.WriteLine("顧客ID,氏名,郵便番号,所在地");
                }

                sw.WriteLine($"{eCustomer.CustomerNumber},{eCustomer.FullName},{eCustomer.EmpAddress.GetPostalCode()},{eCustomer.EmpAddress}");
            }
            return true;
        }
        catch (Exception ex)
        {
            HLog.Except(ex);
            return false;
        }
    }

}
