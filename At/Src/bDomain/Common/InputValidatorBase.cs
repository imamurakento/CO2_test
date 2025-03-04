using CO2.At.Src.bDomain.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.bDomain.eValueObject;


public partial class InputValidatorBase : ObservableObject
{

    [ObservableProperty]
    private bool _isValid = true;

    [ObservableProperty]
    private string _validationErrorMsg = string.Empty;

    protected void ThrowValueObjectExcept(string valueObjectName, string errMsg, string errValue = "")
    {
        ValidationErrorMsg = errMsg;
        IsValid = false;
        throw new CustomValueObjectException(valueObjectName, errMsg, errValue);
    }
    protected void SetError(string valueObjectName, string errMsg, string errValue = "")
    {
        HLog.Err($"ValueObject名: {valueObjectName} :エラー内容:{errMsg};: エラーの値: {errValue}");
        ValidationErrorMsg = errMsg;
        IsValid = false;
#if DB_MIGRATION        //有効にすると現状、チェック処理で落ちる。
        ThrowValueObjectExcept(valueObjectName, errMsg, errValue);
#endif

    }

    protected void SetNormal()
    {
        ValidationErrorMsg = string.Empty;
        IsValid = true;
    }


}
