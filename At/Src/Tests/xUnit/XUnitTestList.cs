using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CO2.At.Src.aFunc.Customer;
using CO2.At.Src.aFunc.Employee;
using CO2.At.Src.bDomain.aLogics;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Entities;
using CO2.At.Src.bDomain.eValueObject;
using CO2.At.Src.bDomain.Helpers;
using CO2.At.Src.bInfrastructure.DB;
using Microsoft.Data.SqlClient;
using Xunit;

#if UNIT_TEST

namespace CO2.At.Src.Tests.xUnit;

public class XUnitTestList
{


    public void TExecute()
    {

        HFontSize hFont = new HFontSize();

        float fontSizeInPoints = 16f;
        float fontSizeInPixels = hFont.GetFontSizeInPixels(fontSizeInPoints);

        //Test_ECustomer();

        //XUnitValueObject xUnitValueObject = new ();
        //xUnitValueObject.ExecuteAllMethod();
        Test_Resources();

        if (GIc.GetAppSetting().IsDbUse)
        {
            //Test_OrganizationParent();
        }
    }


    public void Test_Resources()
    {
        //リソース関係
        //string resourceValue = HRreources.GetStringResource("Space-1");
        //if (!HRreources.LoadMajorColors())
        //{
        //    Console.WriteLine("Failed to load colors.");
        //}

        HRreources.SetColor("ColorBase", Color.FromArgb("#FFE8B6"));
        HRreources.SetColor("ColorMain", Color.FromArgb("#77B254"));
        HRreources.SetColor("ColorAccent", Color.FromArgb("#5B913B"));

    }

    //public void Test_ECustomer()
    //{
    //    ECustomer customer = GIc.VmCmr.GetDetailCustomerTest();

    //    customer.EmpPhoneNumber.FirstNumber = "06";
    //    customer.EmpPhoneNumber.MiddleNumber = "6208";
    //    customer.EmpPhoneNumber.LastNumber = "2607";


    //}


}
#endif
