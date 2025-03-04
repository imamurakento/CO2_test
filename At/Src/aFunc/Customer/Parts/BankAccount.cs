using System.Collections.ObjectModel;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.eValueObject;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CO2.At.Src.aFunc.Customer.Parts;

public partial class BankAccounts : InputValidatorBase, IInputValidator
{

    ////口座番号のピッカーはタブが切り替わったときに、データバィンディングのケースでは、おそらくNULLが発生し、正しくリストからデータが反映されないケースが発生する。
    ////XAMLで直接記載したケースでは現象は発生しない。タブを利用するケースが顧客詳細画面しかないこと、および口座種別のデータが頻繁に変わることは想定しにくいことから、XAMLで直接実装する。

    [ObservableProperty]
    private string _bankName=string.Empty;
    [ObservableProperty]
    private string _branchName=string.Empty;
    [ObservableProperty]
    private string _accountNumber=string.Empty;
    [ObservableProperty]
    private int _selectedAccountKind=0;

    public void SetBankName(string bankName)
    {
        BankName = bankName;
    }
    public void SetBranchName(string branchName)
    {
        BranchName = branchName;
    }
    public void SetAccountKind(byte accountIndex)
    {
        if (accountIndex== 1)
        {
            SelectedAccountKind = 0;
        }
        else if (accountIndex == 2)
        {
            SelectedAccountKind = 1;
        }
    }

    public void SetAccountNumber(string accountNumber)
    {
        if (int.TryParse(accountNumber, out var value))
        {
            AccountNumber = accountNumber;
        }
        else
        {
            SetError(nameof(BankAccounts), "口座番号が数値以外の値で入力されています。", accountNumber);
        }
    }

    public string GetBankName()
    {
        return BankName;
    }
    public string GetBranchName()
    {
        return BranchName;
    }
    public int GetAccountKind()
    {
        return SelectedAccountKind;
    }

    public string GetAccountNumber()
    {
        return AccountNumber;

    }


    public bool IsValidated()
    {
        throw new NotImplementedException();
    }
}
