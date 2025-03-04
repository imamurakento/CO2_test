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
        // ���K�\���Ō`�����`�F�b�N���A����
        var match = Regex.Match(fullValue, @"^(?<firstNumber>[0-9]{2,4})-(?<middleNumber>[0-9]{1,4})-(?<lastNumber>[0-9]{4})$");
        if (!match.Success)
        {
            FirstNumber = string.Empty;
            MiddleNumber = string.Empty;
            LastNumber = string.Empty;
            SetPhoneError("�d�b�ԍ��̌`�����s���ł�4�B��: 06-6208-2607", fullValue);
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


        ////https://akinov.hatenablog.com/entry/2017/05/31/194421 ���Q�l�ɐ��K�\�����C���A�S�̃p�^�[�����ꂼ����`�F�b�N���Ă����B�S�ĊY�����Ȃ���΃G���[�Ƃ����������W�b�N�֐����Ăяo���A����𗘗p����`�ɏC������B

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
            SetPhoneError("���{�̈�ʂ̓d�b�ԍ����T�|�[�g����d�b�ԍ��͈̔͂ł͂���܂���B", phoneNumberAll);
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
        // �S�p�����̃`�F�b�N
        if (Regex.IsMatch(chkValue, @"[^\x00-\x7F]"))
        {
            // ASCII �͈͊O�̕������܂܂�Ă���
            SetPhoneError("�d�b�ԍ��ɐ����ȊO�̌`�����܂܂�Ă��܂��B��: 06-6208-2607", chkValue);
            IsValidFirst = false;
        }
        else if (Regex.IsMatch(chkValue, "^0(\\d{1}|\\d{2}|\\d{3}|\\d{4})$") || Regex.IsMatch(chkValue, "^0[5789]0$") || (chkValue == "0120"))
        {
            IsValidFirst = true;
        }
        else
        {
            SetPhoneError("�d�b�ԍ��̌`�����s���ł�1�B��: 06-6208-2607", chkValue);
            IsValidFirst = false;
        }
        SetPhoneNormal();
        return IsValidFirst;
    }
    private bool IsValidateMiddle(string chkValue)
    {
        // �S�p�����̃`�F�b�N
        if (Regex.IsMatch(chkValue, @"[^\x00-\x7F]"))
        {
            // ASCII �͈͊O�̕������܂܂�Ă���
            SetPhoneError("�d�b�ԍ��ɐ����ȊO�̌`�����܂܂�Ă��܂��B��: 06-6208-2607", chkValue);
            IsValidMiddle = false;
        }
        else if (Regex.IsMatch(chkValue, "^[0-9]{4}$|^[0-9]{3}$|^[0-9]{2}$"))
        {
            IsValidMiddle = true;
        }
        else
        {
            SetPhoneError("�d�b�ԍ��̌`�����s���ł�2�B��: 06-6208-2607", chkValue);
            IsValidMiddle = false;
        }
        SetPhoneNormal();
        return IsValidMiddle;
    }

    private bool IsValidateLast(string chkValue)
    {
        // �S�p�����̃`�F�b�N
        if (Regex.IsMatch(chkValue, @"[^\x00-\x7F]"))
        {
            // ASCII �͈͊O�̕������܂܂�Ă���
            SetPhoneError("�d�b�ԍ��ɐ����ȊO�̌`�����܂܂�Ă��܂��B��: 06-6208-2607", chkValue);
            IsValidLast = false;
        }
        else if (Regex.IsMatch(chkValue, "^[0-9]{4}$"))
        {
            IsValidLast = true;
        }
        else
        {
            SetPhoneError("�d�b�ԍ��̌`�����s���ł�3�B��: 06-6208-2607", chkValue);
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
