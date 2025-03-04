using System.Diagnostics;
using System.Text.RegularExpressions;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Windows.Graphics.Printing;

namespace CO2.At.Src.bDomain.eObservableObject;

public partial class InpubValidatorGeneral : InputValidatorBase, IInputValidator
{
    [ObservableProperty]
    private string _fullName = string.Empty;

    public bool IsValidated()
    {
        return true;
    }

    public void SetGeneralError(string errMsg, string errValue) => SetError(nameof(InpubValidatorGeneral), errMsg, errValue);

    public void SetGeneralNormal()
    {
        SetNormal();
    }



}
