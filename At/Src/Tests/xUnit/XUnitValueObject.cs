using CO2.At.Src.aFunc.Customer.Parts;
using CO2.At.Src.bDomain.Common;
using CO2.At.Src.bDomain.Helpers;
using Xunit;

#if UNIT_TEST

namespace CO2.At.Src.Tests.xUnit;

internal class XUnitValueObject : XUnitAbstract
{

    public void T生年月日()
    {
        //ToDo:年号別の日本語テスト関数に分けてXUnitValueObjectで実装し、自動的に処理が呼ばれるようにする。
        Birthday vOBirthday = new ();
        vOBirthday.SetBirthday(Convert.ToDateTime("1870 /02/01"));
        Assert.Equal("明治", vOBirthday.SelectedEra);
        vOBirthday.GetBirthday();
        Assert.Equal("1870", vOBirthday.Year);

        vOBirthday.SetBirthday(Convert.ToDateTime("1920/02/01"));
        Assert.Equal("大正", vOBirthday.SelectedEra);
        vOBirthday.GetBirthday();
        Assert.Equal("1920", vOBirthday.Year);

        vOBirthday.SetBirthday(Convert.ToDateTime("1989/01/01"));
        Assert.Equal("昭和", vOBirthday.SelectedEra);
        vOBirthday.GetBirthday();
        Assert.Equal("1989", vOBirthday.Year);

        vOBirthday.SetBirthday(Convert.ToDateTime("1989/02/01"));
        Assert.Equal("平成", vOBirthday.SelectedEra);
        vOBirthday.GetBirthday();
        Assert.Equal("1989", vOBirthday.Year);

        vOBirthday.SetBirthday(Convert.ToDateTime("2019/05/01"));
        Assert.Equal("令和", vOBirthday.SelectedEra);
        vOBirthday.GetBirthday();
        Assert.Equal("2019", vOBirthday.Year);

    }

    public void 郵便番号()
    {
        HLog.Info("郵便番号テスト実行");

        //VOPostalCode2 vOPostalCode = new ("123-4567");
        //Assert.True(vOPostalCode.IsValidPostalCode("536", "0022"));
        //Assert.False(vOPostalCode.IsValidPostalCode("5", "0022"));
        //Assert.False(vOPostalCode.IsValidPostalCode("536", "AAA"));
    }



}
#endif