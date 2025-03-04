using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Identity.Client;

#pragma warning disable SA1201

namespace CO2.At.Src.aFunc.Employee;

public partial class ETreeNode : ObservableObject
{
    //private int _hierarchyId = 0;

    public int NodeId { get; set; }= 0;

    public int HierarchyKind = 0;

    [ObservableProperty]
    private string _nodeName = string.Empty;

    //public int HierarchyUpperID = 0;
    //public int HierarchyLowerID = 0;

    private string _valueName = string.Empty;
    public string ValueName
    {
        get
        {
            // NodeName に値を設定
            return _valueName;
        }
        set
        {
            _valueName = value;
            NodeName = _valueName;
        }
    }


    private string _valueType = string.Empty;
    public string ValueType
    {
        get
        {
            // NodeName に値を設定
            return _valueType;
        }
        set
        {
            _valueType = value;
            if (_valueType == "Parent")
            {
                HierarchyKind = 71;
            }
            else if (_valueType == "Child")
            {
                HierarchyKind = 72;
            }
            else if (_valueType == "Employee")
            {
                HierarchyKind = 73;
            }

        }
    }

    public IList<ETreeNode> Children { get; set; } = new ObservableCollection<ETreeNode>();

    //public void Set(int hierarchyID)
    //{
    //    _hierarchyId = hierarchyID;

    //    if (hierarchyID == 1)
    //    {
    //        HierarchyUpperID = 0;

    //    }
    //}

}

#pragma warning disable SA1201
