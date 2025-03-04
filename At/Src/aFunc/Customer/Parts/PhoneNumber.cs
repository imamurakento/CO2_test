using System.Diagnostics;
using System.Text.RegularExpressions;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Windows.Graphics.Printing;

namespace CO2.At.Src.aFunc.Customer.Parts;

public partial class PhoneNumber : InputValidatorBase, IInputValidator
{
    [ObservableProperty]
    private string _firstNumber=string.Empty;
    [ObservableProperty]
    private string _middleNumber=string.Empty;
    [ObservableProperty]
    private string _lastNumber = string.Empty;
    [ObservableProperty]
    private bool _isValidFirst = true;
    [ObservableProperty]
    private bool _isValidMiddle = true;
    [ObservableProperty]
    private bool _isValidLast = true;
    [ObservableProperty]
    private string _fullName = string.Empty;

    public void SetPhoneNumber(string fullValue)
    {
        // 正規表現で形式をチェックし、分割
        var match = Regex.Match(fullValue, @"^(?<firstNumber>[0-9]{2,4})-(?<middleNumber>[0-9]{1,4})-(?<lastNumber>[0-9]{4})$");
        if (!match.Success)
        {
            FirstNumber = string.Empty;
            MiddleNumber = string.Empty;
            LastNumber = string.Empty;
            SetPhoneError("電話番号の形式が不正です4。例: 06-6208-2607", fullValue);
        }
        else
        {
            FirstNumber = match.Groups["firstNumber"].Value;
            MiddleNumber = match.Groups["middleNumber"].Value;
            LastNumber = match.Groups["lastNumber"].Value;

            IsValidated();
        }
        FullName = GetPhoneNumber();
    }

    public string GetPhoneNumber()
    {
        string fullName = FirstNumber + "-" + MiddleNumber + "-" + LastNumber;
        return fullName;
    }

    public bool IsValidated()
    {

        if (!(IsValideteFirst(FirstNumber) && IsValidateMiddle(MiddleNumber) && IsValidateLast(LastNumber)))
        {
            return false;
        }

        string phoneNumberAll = FirstNumber+MiddleNumber+LastNumber;

        bool isOk;


        ////https://akinov.hatenablog.com/entry/2017/05/31/194421 を参考に正規表現を修正、４つのパターンそれぞれをチェックしていく。全て該当しなければエラーという内部ロジック関数を呼び出し、それを利用する形に修正する。

        if (Regex.IsMatch(phoneNumberAll, "^0[5789]0[-(]?\\d{4}[-)]?\\d{4}$"))
        {
            isOk = true;
        }
        else if (Regex.IsMatch(phoneNumberAll, "^0(\\d{1}[-(]?\\d{4}|\\d{2}[-(]?\\d{3}|\\d{3}[-(]?\\d{2}|\\d{4}[-(]?\\d{1})[-)]?\\d{4}$"))
        {
            isOk = true;
        }
        else if (Regex.IsMatch(phoneNumberAll, "/^0120[-(]?\\d{3}[-)]?\\d{3}$/"))
        {
            isOk = true;
        }
        else
        {
            IsValidFirst = false;
            IsValidMiddle = false;
            IsValidLast = false;
            SetPhoneError("日本の一般の電話番号がサポートする電話番号の範囲ではありません。", phoneNumberAll);
            return false;
        }

        if (isOk)
        {
            IsValidFirst = true;
            IsValidMiddle = true;
            IsValidLast = true;
            SetNormal();
            return true;
        }
        return false;
    }

    private bool IsValideteFirst(string chkValue)
    {
        // 全角文字のチェック
        if (Regex.IsMatch(chkValue, @"[^\x00-\x7F]"))
        {
            // ASCII 範囲外の文字が含まれている
            SetPhoneError("電話番号に数字以外の形式が含まれています。例: 06-6208-2607", chkValue);
            IsValidFirst = false;
        }
        else if (Regex.IsMatch(chkValue, "^0(\\d{1}|\\d{2}|\\d{3}|\\d{4})$") || Regex.IsMatch(chkValue, "^0[5789]0$") || (chkValue == "0120"))
        {
            IsValidFirst = true;
        }
        else
        {
            SetPhoneError("電話番号の形式が不正です1。例: 06-6208-2607", chkValue);
            IsValidFirst = false;
        }
        SetPhoneNormal();
        return IsValidFirst;
    }
    private bool IsValidateMiddle(string chkValue)
    {
        // 全角文字のチェック
        if (Regex.IsMatch(chkValue, @"[^\x00-\x7F]"))
        {
            // ASCII 範囲外の文字が含まれている
            SetPhoneError("電話番号に数字以外の形式が含まれています。例: 06-6208-2607", chkValue);
            IsValidMiddle = false;
        }
        else if (Regex.IsMatch(chkValue, "^[0-9]{4}$|^[0-9]{3}$|^[0-9]{2}$"))
        {
            IsValidMiddle = true;
        }
        else
        {
            SetPhoneError("電話番号の形式が不正です2。例: 06-6208-2607", chkValue);
            IsValidMiddle = false;
        }
        SetPhoneNormal();
        return IsValidMiddle;
    }

    private bool IsValidateLast(string chkValue)
    {
        // 全角文字のチェック
        if (Regex.IsMatch(chkValue, @"[^\x00-\x7F]"))
        {
            // ASCII 範囲外の文字が含まれている
            SetPhoneError("電話番号に数字以外の形式が含まれています。例: 06-6208-2607", chkValue);
            IsValidLast = false;
        }
        else if (Regex.IsMatch(chkValue, "^[0-9]{4}$"))
        {
            IsValidLast = true;
        }
        else
        {
            SetPhoneError("電話番号の形式が不正です3。例: 06-6208-2607", chkValue);
            IsValidLast = false;
        }
        SetPhoneNormal();
        return IsValidLast;
    }

    [RelayCommand]
    private void ValidateFistNumber(string chkValue) => IsValideteFirst(chkValue);

    [RelayCommand]
    private void ValidateMiddleNumber(string chkValue) => IsValidateMiddle(chkValue);

    [RelayCommand]
    private void ValidateLastNumber(string chkValue) => IsValidateLast(chkValue);
    private void SetPhoneError(string errMsg, string errValue) => SetError(nameof(PhoneNumber), errMsg, errValue);

    private void SetPhoneNormal()
    {
        if (IsValidFirst && IsValidMiddle && IsValidLast)
        {
            FullName = GetPhoneNumber();
            SetNormal();
        }
    }


}
