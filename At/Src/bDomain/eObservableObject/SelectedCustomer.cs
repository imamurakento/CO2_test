using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.bDomain.eObservableObject;

public partial class SelectedCustomer : ObservableObject
{

    [ObservableProperty]
    private string _customerNumber = string.Empty;

    [ObservableProperty]
    private string _fullName = string.Empty;

    [ObservableProperty]
    private string _empPhoneNumber = string.Empty;

    [ObservableProperty]
    private string _psnPhoneNumber = string.Empty;

    [ObservableProperty]
    private string _psnMobileNumber = string.Empty;

    public SelectedCustomer(string customerNumber, string fullName, string empPhoneNumber,string psnPhoneNumber, string psnMobileNumber)
    {
        CustomerNumber = customerNumber;
        FullName = fullName;
        EmpPhoneNumber = empPhoneNumber;
        PsnPhoneNumber = psnPhoneNumber;
        PsnMobileNumber = psnMobileNumber;
    }
}
