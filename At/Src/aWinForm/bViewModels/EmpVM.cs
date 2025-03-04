using System.Collections.ObjectModel;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.Employee;
using CO2.At.Src.aWinForm.aViews;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CO2.At.Src.aWinForm.bViewModels;

internal partial class EmpVM : ObservableObject
{
    private readonly IEmp _employeeRepo;
    private readonly HWindow _hWindow;

    [ObservableProperty]
    private ObservableCollection<ETreeNode>? _treeNodeList;

    [ObservableProperty]
    private ETreeNode _selectedNode = new ();

    [ObservableProperty]
    private ObservableCollection<ETreeNode> _valueName;

    [ObservableProperty]
    private ObservableCollection<ETreeNode> _children;

    [ObservableProperty]
    private EEmp _employeeDetail;

    [ObservableProperty]
    private CrudPrm _crudTarget;

    public LoginUser? LoginUser;

    private List<EParent>? _parentList;
    private List<EChild>? _childList ;
    private List<EEmp>? _employeeList;


    public EmpVM(IEmp employeeRepo)
    {

        LoginUser = null;

        _employeeRepo = employeeRepo;
        _hWindow = GIc.GetWindow();

        _treeNodeList = new ObservableCollection<ETreeNode>();

        ////親組織・子組織・社員単位のデータを全て読み込み(詳細画面でも利用)
        _parentList = _employeeRepo.LoadP();
        _childList = _employeeRepo.LoadC();
        _employeeList = _employeeRepo.LoadE();

        ////TreeViewの階層表示データはデータバィンディングでユーザーが選択した項目を取得するため、全てETreeNode型で生成する。
        ////別途、親組織のEntity単位で所属している子組織以下のEntityデータを格納する。(詳細画面でも利用)
        foreach (var parent in _parentList)
        {
            ETreeNode eNodeParent = new ETreeNode();

            eNodeParent.NodeId = parent.OrgParentId;
            eNodeParent.ValueName = parent.OrgParentName;
            eNodeParent.ValueType = "Parent";

            var targetChilds = _childList.Where(c => c.OrgParentId == parent.OrgParentId);
            //List<EChild> targetChilds = (List<EChild>)_childList.Where(c => c.OrganizationParentId == parent.OrgParentId);
            //parent.OrgChildList = targetChilds;

            foreach (var child in targetChilds)
            {

                ETreeNode eNodeChild = new ETreeNode();

                eNodeChild.NodeId = child.OrgChildId;
                eNodeChild.ValueName = child.OrgChildName;
                eNodeChild.ValueType = "Child";

                var targetEmployees = _employeeList.Where(e => e.OrgChildId == child.OrgChildId);

                foreach (var employee in targetEmployees)
                {

                    ETreeNode eNodeEmployee = new ETreeNode();

                    eNodeEmployee.NodeId = employee.EmpId;
                    eNodeEmployee.ValueName = employee.EmpName;
                    eNodeEmployee.ValueType = "Employee";
                    eNodeChild.Children.Add(eNodeEmployee);
                }

                eNodeParent.Children.Add(eNodeChild);


            }
            _treeNodeList.Add(eNodeParent);

        }

    }
    public bool ChkLogin(string userName, string passWord)
    {
        if (_employeeRepo.ChkLogin(userName, passWord))
        {
            LoginUser = new LoginUser(userName, passWord);
            return true;
        }
        return false;
    }

    [RelayCommand]
    private void SelectParent()
    {

        ObservableCollection<ETreeNode> pareTmp = ValueName;

        ObservableCollection<ETreeNode> chldTMmp =Children;

        //HWindow.CreateWindowRate(new Vw621_OrgParent(), 0.6, 0.4);
    }

    private EEmp? FindEmployee(int iD)
    {
        return _employeeList?.FirstOrDefault(e => e.EmpId == iD);
    }

    //[RelayCommand]
    //private void CRUD(string crudKind)
    //{

    //    ETreeNode Selected = SelectedNode;

    //    int hierarchyKind = Selected.HierarchyKind;
    //    string nameTmp = Selected.ValueName;

    //    //if (crudKind == Def.CRUDDETAIL)
    //    //{

    //    //}

    //    int kind = Convert.ToInt32(crudKind);

    //    if (hierarchyKind == 71)
    //    {
    //        HWindow.CreateWindowRate(new Vw621_OrgParent(), 0.6, 0.4);
    //    }
    //    else if (hierarchyKind == 72)
    //    {
    //        HWindow.CreateWindowRate(new Vw622_OrgChild(), 0.6, 0.4);
    //    }
    //    else if (hierarchyKind == 73)
    //    {
    //        HWindow.CreateWindowRate(new Vw623_Employee(), 0.6, 0.4);
    //    }

    //    //HWindow.CreateWindowRate(new Vw622_OrgChild(), 0.6, 0.4);
    //}

    [RelayCommand]
    private async Task CRUD(string crudKind)
    {

        ETreeNode selected = SelectedNode;

        int hierarchyKind = selected.HierarchyKind;
        string nameTmp = selected.ValueName;

        //if (crudKind == Def.CRUDDETAIL)
        //{

        //}

        int kind = Convert.ToInt32(crudKind);

        CrudTarget = new CrudPrm(kind);

        if (hierarchyKind == 71)
        {
            _hWindow.CreateWindow(new Vw621_OrgParent(), 0.6, 0.4);
        }
        else if (hierarchyKind == 72)
        {
            _hWindow.CreateWindow(new Vw622_OrgChild(), 0.6, 0.6);
        }
        else if (hierarchyKind == 73)
        {
            _hWindow.CreateWindow(new Vw623_Employee(), 0.6, 0.4);
            //await Application.Current.MainPage.Navigation.PushAsync(new Vw623_Employee());
        }

        //await Shell.Current.GoToAsync($"Vw623_Employee");//NG

    }


    [RelayCommand]
    private void SelectChild()
    {
        _hWindow.CreateWindow(new Vw622_OrgChild(), 0.6, 0.4);
    }

    [RelayCommand]
    private void SelectEmployee()
    {
        _hWindow.CreateWindow(new Vw623_Employee(), 0.6, 0.4);
    }

    [RelayCommand]
    private void SelectUser()
    {
        _hWindow.CreateWindow(new Vw624_User(), 0.6, 0.4);
    }



    public bool CreateOrganizationParent(EParent eOrganizationParent)
    {
        if (eOrganizationParent == null)
        {
            return false;
        }

        if (_employeeRepo.CreateP(eOrganizationParent))
        {
            return true;
        }
        return false;
    }

    public EParent? ReadOrganizationParent(int target_number)
    {
        return _employeeRepo.ReadP(target_number);
    }

    public bool UpdateOrganizationParent(EParent? eOrganizationParent)
    {
        if (eOrganizationParent == null)
        {
            return false;
        }
        return _employeeRepo.UpdateP(eOrganizationParent);
    }

    public bool DeleteOrganizationParent(int target_number)
    {
        if (_employeeRepo.DeleteP(target_number))
        {
            return true;
        }
        return false;
    }

    public int GetMaxOrganizationId()
    {
        return _employeeRepo.GetMaxOrganizationId();
    }





}

//public class MyItem
//{
//    public MyItem(string name, string valueType) // For easy initialization (optional)
//    {
//        Name = name;
//        ValueType = valueType;
//    }

//    public string Name { get; set; }
//    public string ValueType { get; set; } // 親か子を表すプロパティ
//    public IList<MyItem> Children { get; set; } = new ObservableCollection<MyItem>();
//}
