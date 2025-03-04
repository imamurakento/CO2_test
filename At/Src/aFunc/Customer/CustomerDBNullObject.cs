using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.Helpers;
using Microsoft.Data.SqlClient;

[assembly: InternalsVisibleTo("xUnitCO2V2")]

namespace CO2.At.Src.aFunc.Customer;

internal class CustomerDBNullObject : ICustomer
{
    public CustomerDBNullObject()
    {
        HLog.Warn("CustomerNullオブジェクト生成");
    }


    List<ECustomer>? ICustomer.Load()
    {
        HLog.Warn("CustomerNullオブジェクト:Load:呼び出しエラー");
        return null;
    }

    public List<ECustomer>? InitialSearch(string initial)
    {
        HLog.Warn("CustomerNullオブジェクト:InitialSearch:呼び出しエラー");
        return null;
    }

    public List<ECustomer>? SearchByVowel(string vowelKindName)
    {
        HLog.Warn("CustomerNullオブジェクト:PartialSearch:呼び出しエラー");
        return null;
    }

    public bool Create(ECustomer eCustomer)
    {
        HLog.Warn("CustomerNullオブジェクト:Create:呼び出しエラー");
        return false;
    }

    public ECustomer? Read(int target_number)
    {
        HLog.Warn("CustomerNullオブジェクト:Read:呼び出しエラー");
        return null;
    }

    public bool Update(ECustomer eCustomer)
    {
        HLog.Warn("CustomerNullオブジェクト:Update:呼び出しエラー");
        return false;
    }

    ////削除フラグを更新する。物理的な削除は実施しない。

    public bool Delete(int target_number)
    {
        HLog.Warn("CustomerNullオブジェクト:Delete:呼び出しエラー");
        return false;
    }

    public int GetNewID()
    {
        return -1;
    }

    public List<ECustomer>? PartialSearch(string fullName, string furigana, string personalPhone, string companyPhone)
    {
        /*throw new NotImplementedException()*/
        return null;
    }

    public List<ECustomer> LoadDeleteCustomers()
    {
        throw new NotImplementedException();
    }
}
