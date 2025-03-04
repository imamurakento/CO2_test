using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.eObservableObject;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aWinForm.bViewModels;

public partial class Vw1VM : ObservableObject
{
    private ETategyokus? _tategyokus;

    [ObservableProperty]
    private SelectedCustomer _targetCustomer;

    public ObservableCollection<ETategyoku>? TategyokuList { get; set; }


    public Vw1VM()
    {
        TategyokuList = GetTategyokus().TategyokuList;
        TargetCustomer = new SelectedCustomer("未選択", "---", "---", "---", "---");
    }

    public SelectedCustomer GetSelectedCustomer() => TargetCustomer;
    public void SetSelectedCustomer(SelectedCustomer selectedCustomer)
    {
        TargetCustomer = selectedCustomer;
    }

    private ETategyokus? GetTategyokus()
    {
        if (_tategyokus == null)
        {
            _tategyokus = new ETategyokus();
        }

        return _tategyokus;
    }


}
