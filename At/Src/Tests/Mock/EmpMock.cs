using CO2.At.Src.aFunc.Employee;
using CommunityToolkit.Mvvm.ComponentModel;

#if UNIT_TEST

#pragma warning disable CS8603

namespace CO2.At.Src.Tests.Mock;

internal partial class EmpMock : ObservableObject, IEmp
{

    private List<EParent>? _orgParentList;
    private List<EChild>? _orgChildList;
    private List<EEmp>? _employeeList;

    public EmpMock()
    {
        _orgParentList = new List<EParent>
        {
            new () { OrgParentId = 1, OrgParentName = "親1" },
            new () { OrgParentId = 2, OrgParentName = "親2" },
            new () { OrgParentId = 3, OrgParentName = "親3" },
        };

        _orgChildList = new List<EChild>
        {
            new () { OrgChildId = 1, OrgChildName = "子1", Furigana = "コイチ", OrgParentId = 1 },
            new () { OrgChildId = 2, OrgChildName = "子2", Furigana = "コニイ", OrgParentId = 1 },
            new () { OrgChildId = 3, OrgChildName = "子3", Furigana = "コサン", OrgParentId = 2 },
        };

        _employeeList = new List<EEmp>
        {
            new () { EmpId = 1, EmpName = "社員1", OrgChildId = 1 },
            new () { EmpId = 2, EmpName = "社員2", OrgChildId = 1 },
            new () { EmpId = 3, EmpName = "社員3", OrgChildId = 2 },
        };

        //ToDo:イメージ：_OrgParentListをまず取得し、そのリストでループを回し、親階層に所属する全ての子を_OrgChildListに格納する。
        //_OrgChildListのリストをループで回し、子階層に所属する全ての社員を社員リストに格納する。
        //親のリストはViewModelが持ち、子のリストは親が持ち、社員のリストは子が持つ。

        foreach (var p in _orgParentList)
        {
            var targetChilds = _orgChildList.Where(c => c.OrgParentId == p.OrgParentId);

            foreach (var c in targetChilds)
            {
                var result3 = _employeeList.Where(e => e.OrgChildId == c.OrgChildId);
            }
        }

    }


    public List<EParent> LoadP()
    {
        return _orgParentList;
    }
    public List<EChild> LoadC()
    {
        return _orgChildList;
    }
    public List<EEmp> LoadE()
    {
        return _employeeList;
    }



    public bool ChkLogin(string userName, string password)
    {
        if (userName == "1" && password == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CreateOrganizationParent(EParent eOrganizationParent)
    {
        if (eOrganizationParent == null)
        {
            return false;
        }
        return true;
    }

    public EParent? ReadOrganizationParent(int target_num)
    {
        EParent eOrganization = new ();

        eOrganization.Set(
            orgParentId: 0,
            orgParentName: "テスト");

        return eOrganization;
    }

    public bool UpdateOrganizationParent(EParent eOrganizationParent)
    {
        if (eOrganizationParent == null)
        {
            return false;
        }
        return true;
    }

    public bool DeleteOrganizationParent(int targetID)
    {
        return true;
    }

    public int GetMaxOrganizationId()
    {
        return -1;
    }

    public bool CreateP(EParent eOrganizationParent)
    {
        return true;
    }

    public EParent? ReadP(int targetID)
    {
        return _orgParentList.FirstOrDefault();
    }

    public bool UpdateP(EParent eOrganizationParent)
    {
        return true;
    }

    public bool DeleteP(int targetID)
    {
        return true;
    }
}
#pragma warning disable CS8603

#endif
