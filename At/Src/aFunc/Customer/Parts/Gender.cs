using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Customer.Parts;

public partial class Gender : InputValidatorBase
{
    [ObservableProperty]
    private bool _isGenderMan =true;

    [ObservableProperty]
    private bool _isGenderWoMan = false;
    public enum GenderKind : int
    {
        Man = 1,
        Woman = 2,
    }
    public void SetGender(int fullValue)
    {
        if (fullValue == (int)GenderKind.Man)
        {
            IsGenderMan = true;
            IsGenderWoMan = false;
        }
        else if (fullValue == (int)GenderKind.Woman)
        {
            IsGenderMan = false;
            IsGenderWoMan = true;
        }
        else
        {
            SetError(nameof(Address), "性別の値が範囲外です。", fullValue.ToString());
        }
    }

    public GenderKind GetGender()
   {
        if (IsGenderMan)
        {
            return GenderKind.Man;
        }
        else
        {
            return GenderKind.Woman;
        }
    }

}
