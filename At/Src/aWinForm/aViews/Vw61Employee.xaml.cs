using CO2.At.Src.aFunc.Employee;
using CO2.At.Src.aWinForm.bViewModels;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Helpers;
using Application = Microsoft.Maui.Controls.Application;


namespace CO2.At.Src.aWinForm.aViews;

public partial class Vw61Employee : ContentPage
{
    private readonly EmpVM? _vmEmployee;
    public Vw61Employee()
    {
        //InitializeComponent();
        //EmployeeViewModel viewModel = new EmployeeViewModel();
        //BindingContext = viewModel;
        try
        {
            InitializeComponent();

            _vmEmployee = GIc.GetEmpVm();

            BindingContext = _vmEmployee;

        }
        catch (Exception ex)
        {
            HLog.Except(ex);
        }
    }

    private void TreeView_ParentChanged(object sender, EventArgs e)
    {
        try
        {
            if (_vmEmployee == null)
            {
                return;
            }

            if (_vmEmployee.SelectedNode == null)
            {
                return;
            }

            ETreeNode selected = _vmEmployee.SelectedNode;

        }
        catch (Exception)
        {

            //throw;
        }

    }

    private void TreeView_ParentChanging(object sender, ParentChangingEventArgs e)
    {
        try
        {
            if (_vmEmployee == null)
            {
                return;
            }

            if (_vmEmployee.SelectedNode == null)
            {
                return;
            }

            ETreeNode selected = _vmEmployee.SelectedNode;


        }
        catch (Exception)
        {

            //throw;
        }
    }

    private void TreeView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        try
        {
            if (_vmEmployee == null)
            {
                return;
            }

            if (_vmEmployee.SelectedNode == null)
            {
                return;
            }

            ETreeNode selected = _vmEmployee.SelectedNode;

        }
        catch (Exception)
        {

            //throw;
        }
    }
}